/// <reference types="@cloudflare/workers-types" />

import type { CreatePredictionBody, PredictionsResponse } from "../../../lib/predictions-api-types";
import { addPrediction, Env, readStore } from "../../_lib/prediction-store";

const JSON_HEADERS = {
  "Content-Type": "application/json; charset=utf-8",
  "Cache-Control": "no-store",
};

export const onRequestGet: PagesFunction<Env> = async ({ env }) => {
  try {
    const store = await readStore(env);
    const body: PredictionsResponse = { predictions: store.predictions };
    return new Response(JSON.stringify(body), {
      status: 200,
      headers: JSON_HEADERS,
    });
  } catch (error) {
    console.error("/api/predictions GET failed", error);
    return new Response(JSON.stringify({ error: "Failed to load predictions" }), {
      status: 500,
      headers: JSON_HEADERS,
    });
  }
};

export const onRequestPost: PagesFunction<Env> = async ({ request, env }) => {
  let payload: unknown;
  try {
    payload = await request.json();
  } catch (error) {
    console.error("Invalid JSON payload", error);
    return new Response(JSON.stringify({ error: "Invalid JSON body" }), {
      status: 400,
      headers: JSON_HEADERS,
    });
  }

  const parsed = parseCreatePayload(payload);
  if (!parsed.ok) {
    return new Response(JSON.stringify({ error: parsed.error }), {
      status: 400,
      headers: JSON_HEADERS,
    });
  }

  try {
    const store = await addPrediction(env, parsed.value);
    const body: PredictionsResponse = { predictions: store.predictions };
    return new Response(JSON.stringify(body), {
      status: 201,
      headers: JSON_HEADERS,
    });
  } catch (error) {
    console.error("/api/predictions POST failed", error);
    return new Response(JSON.stringify({ error: "Failed to add prediction" }), {
      status: 500,
      headers: JSON_HEADERS,
    });
  }
};

type ParsedPayload =
  | { ok: true; value: CreatePredictionBody }
  | { ok: false; error: string };

function parseCreatePayload(input: unknown): ParsedPayload {
  if (typeof input !== "object" || input === null) {
    return { ok: false, error: "Body must be an object" };
  }
  const data = input as Record<string, unknown>;
  const question = typeof data.question === "string" ? data.question : "";
  const icon = typeof data.icon === "string" ? data.icon : "";
  const category = typeof data.category === "string" ? data.category : "";
  if (!question.trim()) {
    return { ok: false, error: "Question is required" };
  }
  if (!icon.trim()) {
    return { ok: false, error: "Icon is required" };
  }
  if (!category.trim()) {
    return { ok: false, error: "Category is required" };
  }
  return {
    ok: true,
    value: {
      question,
      icon,
      category,
    },
  };
}
