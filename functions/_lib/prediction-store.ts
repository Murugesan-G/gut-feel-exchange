/// <reference types="@cloudflare/workers-types" />

import { PREDICTIONS_KV_KEY } from "../../lib/constants";
import { createPrediction } from "../../lib/model";
import type { CreatePredictionInput, Prediction } from "../../types/prediction";

export type Env = {
  PREDICTIONS_KV: KVNamespace;
};

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

export async function readPredictions(env: Env): Promise<Prediction[]> {
  assertEnv(env);
  const raw = await env.PREDICTIONS_KV.get<unknown>(PREDICTIONS_KV_KEY, { type: "json" });
  if (Array.isArray(raw)) return raw as Prediction[];
  return [];
}

async function writePredictions(env: Env, predictions: Prediction[]): Promise<void> {
  assertEnv(env);
  await env.PREDICTIONS_KV.put(PREDICTIONS_KV_KEY, JSON.stringify(predictions));
}

export async function addPrediction(env: Env, input: CreatePredictionInput): Promise<Prediction[]> {
  const current = await readPredictions(env);
  const created = createPrediction(input);
  const next = [created, ...current];
  await writePredictions(env, next);
  return next;
}

export async function recordVote(
  env: Env,
  params: { id: string; side: "yes" | "no"; stake: number },
): Promise<Prediction[]> {
  const current = await readPredictions(env);
  const { id, side, stake } = params;
  const increment = Math.max(0, Math.round(stake));
  const stamp = Date.now();
  const next = current.slice();
  let idx = -1;
  for (let i = 0; i < next.length; i += 1) {
    if (next[i].id === id) {
      idx = i;
      break;
    }
  }
  if (idx === -1) {
    throw new PredictionNotFoundError(id);
  }
  const p = next[idx];
  next[idx] =
    side === "yes"
      ? { ...p, yes: p.yes + increment, updatedAt: stamp }
      : { ...p, no: p.no + increment, updatedAt: stamp };
  await writePredictions(env, next);
  return next;
}
