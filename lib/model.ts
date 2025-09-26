import {
  DEFAULT_CATEGORY,
  DEFAULT_ICON,
  DEFAULT_NEW_VOTE,
  MAX_CATEGORY_LENGTH,
  MAX_QUESTION_LENGTH,
  SEED_SOURCE,
} from "@/lib/constants";
import type { CreatePredictionInput, Prediction } from "@/types/prediction";

export function buildSeedPredictions(now: number = Date.now()): Prediction[] {
  return SEED_SOURCE.map((seed) => ({
    ...seed,
    createdAt: now,
    updatedAt: now,
  }));
}

export function createPrediction(
  input: CreatePredictionInput,
  now: number = Date.now(),
): Prediction {
  const question = sanitizeQuestion(input.question);
  const icon = sanitizeIcon(input.icon);
  const category = sanitizeCategory(input.category);

  return {
    id: generatePredictionId(),
    question,
    icon,
    category,
    yes: DEFAULT_NEW_VOTE,
    no: DEFAULT_NEW_VOTE,
    createdAt: now,
    updatedAt: now,
  };
}

function sanitizeQuestion(raw: string): string {
  return raw.trim().slice(0, MAX_QUESTION_LENGTH);
}

function sanitizeIcon(raw: string): string {
  const trimmed = raw.trim();
  if (trimmed.length === 0) {
    return DEFAULT_ICON;
  }
  const { Segmenter } = Intl as { Segmenter?: typeof Intl.Segmenter };
  if (typeof Segmenter === "function") {
    const segmenter = new Segmenter(undefined, { granularity: "grapheme" });
    const iterator = segmenter.segment(trimmed)[Symbol.iterator]();
    const first = iterator.next();
    if (!first.done && first.value?.segment) {
      return first.value.segment;
    }
  }
  const graphemes = Array.from(trimmed);
  return graphemes[0];
}

function sanitizeCategory(raw: string): string {
  const trimmed = raw.trim();
  if (trimmed.length === 0) {
    return DEFAULT_CATEGORY;
  }
  return trimmed.slice(0, MAX_CATEGORY_LENGTH);
}

function generatePredictionId(): string {
  if (typeof crypto !== "undefined" && typeof crypto.randomUUID === "function") {
    return crypto.randomUUID();
  }
  return `${Date.now().toString(36)}-${Math.random().toString(36).slice(2)}`;
}
