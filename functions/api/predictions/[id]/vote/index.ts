/// <reference types="@cloudflare/workers-types" />

import { JSON_HEADERS } from "../../../../../lib/constants";
import type { PredictionsResponse, VoteBody } from "../../../../../types/prediction";
import { Env, PredictionNotFoundError, recordVote } from "../../../../_lib/prediction-store";

type ParsedVoteBody =
  | { ok: true; value: VoteBody }
  | { ok: false; error: string };

export const onRequestPost: PagesFunction<Env> = async ({ params, request, env }) => {
  const rawId = params?.id;
  const id = Array.isArray(rawId) ? rawId[0] : rawId;
  if (!id) {
    return new Response(JSON.stringify({ error: "Prediction id is required" }), {
      status: 400,
      headers: JSON_HEADERS,
    });
  }

  let payload: unknown;
  try {
    payload = await request.json();
  } catch (error) {
    console.error("Invalid vote JSON", error);
    return new Response(JSON.stringify({ error: "Invalid JSON body" }), {
      status: 400,
      headers: JSON_HEADERS,
    });
  }

  const parsed = parseVoteBody(payload);
  if (!parsed.ok) {
    return new Response(JSON.stringify({ error: parsed.error }), {
      status: 400,
      headers: JSON_HEADERS,
    });
  }

  try {
    const predictions = await recordVote(env, { id, ...parsed.value });
    const body: PredictionsResponse = { predictions };
    return new Response(JSON.stringify(body), {
      status: 200,
      headers: JSON_HEADERS,
    });
  } catch (error) {
    if (error instanceof PredictionNotFoundError) {
      return new Response(JSON.stringify({ error: error.message }), {
        status: 404,
        headers: JSON_HEADERS,
      });
    }
    console.error(`/api/predictions/${id}/vote POST failed`, error);
    return new Response(JSON.stringify({ error: "Failed to record vote" }), {
      status: 500,
      headers: JSON_HEADERS,
    });
  }
};

function parseVoteBody(input: unknown): ParsedVoteBody {
  if (typeof input !== "object" || input === null) {
    return { ok: false, error: "Body must be an object" };
  }
  const data = input as Record<string, unknown>;
  const side = data.side;
  const stakeRaw = data.stake;
  if (side !== "yes" && side !== "no") {
    return { ok: false, error: "Side must be 'yes' or 'no'" };
  }
  if (typeof stakeRaw !== "number" || Number.isNaN(stakeRaw) || stakeRaw <= 0) {
    return { ok: false, error: "Stake must be a positive number" };
  }
  return {
    ok: true,
    value: {
      side,
      stake: stakeRaw,
    },
  };
}
