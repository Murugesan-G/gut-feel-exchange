/// <reference types="@cloudflare/workers-types" />

import {
  CATEGORY_CHOICES,
  ICON_CHOICES,
  JSON_HEADERS,
  type CategoryChoice,
  type IconChoice,
} from "../../../lib/constants";
import type { CreatePredictionInput, PredictionsResponse } from "../../../types/prediction";
import { addPrediction, Env, readPredictions } from "../../_lib/prediction-store";

export const onRequestGet: PagesFunction<Env> = async ({ env }) => {
  try {
    const predictions = await readPredictions(env);
    const body: PredictionsResponse = { predictions };
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
    const predictions = await addPrediction(env, parsed.value);
    const body: PredictionsResponse = { predictions };
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
  | { ok: true; value: CreatePredictionInput }
  | { ok: false; error: string };

const ICON_SET = new Set<string>(ICON_CHOICES);
const CATEGORY_SET = new Set<string>(CATEGORY_CHOICES);

function isIconChoice(value: string): value is IconChoice {
  return ICON_SET.has(value);
}

function isCategoryChoice(value: string): value is CategoryChoice {
  return CATEGORY_SET.has(value);
}

function parseCreatePayload(input: unknown): ParsedPayload {
  if (typeof input !== "object" || input === null) {
    return { ok: false, error: "Body must be an object" };
  }
  const data = input as Record<string, unknown>;
  const question = typeof data.question === "string" ? data.question : "";
  const icon = typeof data.icon === "string" ? data.icon : "";
  const category = typeof data.category === "string" ? data.category : "";
  const trimmedQuestion = question.trim();
  const trimmedIcon = icon.trim();
  const trimmedCategory = category.trim();
  if (!trimmedQuestion) {
    return { ok: false, error: "Question is required" };
  }
  if (!trimmedIcon) {
    return { ok: false, error: "Icon is required" };
  }
  if (!isIconChoice(trimmedIcon)) {
    return { ok: false, error: "Icon selection is invalid" };
  }
  if (!trimmedCategory) {
    return { ok: false, error: "Category is required" };
  }
  if (!isCategoryChoice(trimmedCategory)) {
    return { ok: false, error: "Category selection is invalid" };
  }
  return {
    ok: true,
    value: {
      question: trimmedQuestion,
      icon: trimmedIcon,
      category: trimmedCategory,
    },
  };
}
