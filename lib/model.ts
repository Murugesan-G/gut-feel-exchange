import {
  CATEGORY_CHOICES,
  DEFAULT_CATEGORY,
  DEFAULT_ICON,
  DEFAULT_NEW_VOTE,
  ICON_CHOICES,
  MAX_CATEGORY_LENGTH,
  MAX_QUESTION_LENGTH,
  SEED_SOURCE,
  type CategoryChoice,
  type IconChoice,
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

const ICON_SET = new Set<string>(ICON_CHOICES);
const CATEGORY_SET = new Set<string>(CATEGORY_CHOICES);

function isIconChoice(value: string): value is IconChoice {
  return ICON_SET.has(value);
}

function isCategoryChoice(value: string): value is CategoryChoice {
  return CATEGORY_SET.has(value);
}

function sanitizeIcon(raw: string): string {
  const trimmed = raw.trim();
  if (trimmed.length === 0) {
    return DEFAULT_ICON;
  }
  let candidate: string | undefined;
  const { Segmenter } = Intl as { Segmenter?: typeof Intl.Segmenter };
  if (typeof Segmenter === "function") {
    const segmenter = new Segmenter(undefined, { granularity: "grapheme" });
    const iterator = segmenter.segment(trimmed)[Symbol.iterator]();
    const first = iterator.next();
    if (!first.done && first.value?.segment) {
      candidate = first.value.segment;
    }
  }
  if (!candidate) {
    const graphemes = Array.from(trimmed);
    candidate = graphemes[0];
  }
  if (!candidate) {
    return DEFAULT_ICON;
  }
  return isIconChoice(candidate) ? candidate : DEFAULT_ICON;
}

function sanitizeCategory(raw: string): string {
  const trimmed = raw.trim();
  if (trimmed.length === 0) {
    return DEFAULT_CATEGORY;
  }
  const candidate = trimmed.slice(0, MAX_CATEGORY_LENGTH);
  return isCategoryChoice(candidate) ? candidate : DEFAULT_CATEGORY;
}

function generatePredictionId(): string {
  if (typeof crypto !== "undefined" && typeof crypto.randomUUID === "function") {
    return crypto.randomUUID();
  }
  return `${Date.now().toString(36)}-${Math.random().toString(36).slice(2)}`;
}
