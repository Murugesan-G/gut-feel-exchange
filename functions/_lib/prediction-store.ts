/// <reference types="@cloudflare/workers-types" />

import { PREDICTIONS_KV_KEY } from "../../lib/constants";
import { buildSeedPredictions, createPrediction, normalizePredictionList, truncatePredictions } from "../../lib/model";
import type { CreatePredictionInput, Prediction, PredictionStore } from "../../types/prediction";

export type Env = {
  PREDICTIONS_KV: KVNamespace;
  SEED_DATA_VERSION?: string;
};

const DEFAULT_VERSION = "v2";

export class PredictionNotFoundError extends Error {
  constructor(id: string) {
    super(`Prediction ${id} not found`);
    this.name = "PredictionNotFoundError";
  }
}

function assertEnv(env: Env): asserts env is Required<Env> {
  if (!env?.PREDICTIONS_KV) {
    throw new Error("PREDICTIONS_KV binding is required");
  }
}

type StoredShape = {
  version: string;
  predictions: unknown;
};

function isStoredShape(value: unknown): value is StoredShape {
  if (typeof value !== "object" || value === null) {
    return false;
  }
  const data = value as Record<string, unknown>;
  return typeof data.version === "string" && "predictions" in data;
}

export async function readStore(env: Env): Promise<PredictionStore> {
  assertEnv(env);
  const version = env.SEED_DATA_VERSION || DEFAULT_VERSION;
  const raw = await env.PREDICTIONS_KV.get<StoredShape>(PREDICTIONS_KV_KEY, {
    type: "json",
  });

  if (raw && isStoredShape(raw) && raw.version === version) {
    return {
      version,
      predictions: normalizePredictionList(raw.predictions),
    };
  }

  const seeded: PredictionStore = {
    version,
    predictions: buildSeedPredictions(),
  };
  await writeStore(env, seeded);
  return seeded;
}

async function writeStore(env: Env, store: PredictionStore): Promise<void> {
  assertEnv(env);
  const safePredictions = truncatePredictions(
    store.predictions.map((p) => ({
      ...p,
      yes: Math.max(0, Math.round(p.yes)),
      no: Math.max(0, Math.round(p.no)),
    })),
  );

  await env.PREDICTIONS_KV.put(
    PREDICTIONS_KV_KEY,
    JSON.stringify({ version: store.version, predictions: safePredictions }),
  );
}

export async function mutateStore(
  env: Env,
  mutator: (current: PredictionStore, version: string) => Promise<PredictionStore> | PredictionStore,
): Promise<PredictionStore> {
  const current = await readStore(env);
  const version = env.SEED_DATA_VERSION || current.version || DEFAULT_VERSION;
  const next = await mutator(
    {
      version,
      predictions: [...current.predictions],
    },
    version,
  );
  const normalized: PredictionStore = {
    version,
    predictions: truncatePredictions(next.predictions.map((p) => ({ ...p }))),
  };
  await writeStore(env, normalized);
  return normalized;
}

export async function addPrediction(
  env: Env,
  input: CreatePredictionInput,
): Promise<PredictionStore> {
  return mutateStore(env, (store, version) => {
    const created = createPrediction(input);
    return {
      version,
      predictions: [created, ...store.predictions],
    };
  });
}

export async function recordVote(
  env: Env,
  params: { id: string; side: "yes" | "no"; stake: number },
): Promise<PredictionStore> {
  return mutateStore(env, (store, version) => {
    const { id, side, stake } = params;
    const increment = Math.max(0, Math.round(stake));
    const stamp = Date.now();
    let found = false;
    const updated: Prediction[] = store.predictions.map((prediction) => {
      if (prediction.id !== id) return prediction;
      found = true;
      if (side === "yes") {
        return { ...prediction, yes: prediction.yes + increment, updatedAt: stamp };
      }
      return { ...prediction, no: prediction.no + increment, updatedAt: stamp };
    });
    if (!found) {
      throw new PredictionNotFoundError(id);
    }
    return { version, predictions: updated };
  });
}
