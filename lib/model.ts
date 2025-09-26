import {
  DEFAULT_CATEGORY,
  DEFAULT_ICON,
  DEFAULT_NEW_VOTE,
  MAX_CATEGORY_LENGTH,
  MAX_PREDICTIONS,
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

export function normalizePredictionList(items: unknown): Prediction[] {
  if (!Array.isArray(items)) {
    return [];
  }
  return items
    .map((item) => upgradeStoredPrediction(item))
    .filter((item): item is Prediction => item !== null);
}

export function truncatePredictions(predictions: Prediction[]): Prediction[] {
  if (predictions.length <= MAX_PREDICTIONS) {
    return predictions;
  }
  return [...predictions]
    .sort((a, b) => getOrderTimestamp(b) - getOrderTimestamp(a))
    .slice(0, MAX_PREDICTIONS);
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

function upgradeStoredPrediction(value: unknown): Prediction | null {
  if (typeof value !== "object" || value === null) {
    return null;
  }

  const data = value as Record<string, unknown>;
  const { id, question, icon, category, yes, no, createdAt } = data;
  if (
    typeof id !== "string" ||
    typeof question !== "string" ||
    typeof icon !== "string" ||
    typeof category !== "string" ||
    typeof yes !== "number" ||
    typeof no !== "number" ||
    typeof createdAt !== "number"
  ) {
    return null;
  }

  const timestamp = normalizeTimestamp(createdAt);
  const updatedAt = typeof data.updatedAt === "number" ? normalizeTimestamp(data.updatedAt) : timestamp;

  return {
    id,
    question: sanitizeQuestion(question),
    icon: sanitizeIcon(icon),
    category: sanitizeCategory(category),
    yes,
    no,
    createdAt: timestamp,
    updatedAt,
  };
}

function normalizeTimestamp(raw: number): number {
  if (!Number.isFinite(raw)) {
    return Date.now();
  }
  return Math.max(0, Math.round(raw));
}

function getOrderTimestamp(prediction: Prediction): number {
  return prediction.updatedAt ?? prediction.createdAt;
}
