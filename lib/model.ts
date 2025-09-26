import type { CreatePredictionInput, Prediction } from "@/types/prediction";

const MAX_QUESTION_LENGTH = 120;
const MAX_CATEGORY_LENGTH = 32;
const DEFAULT_NEW_VOTE = 50;
const MAX_PREDICTIONS = 500;

export const DEFAULT_CATEGORY = "Tech";

const SEED_SOURCE = [
  {
    id: "tater-tots",
    question: "Will school lunch have tater tots next week?",
    icon: "🥔",
    category: "Food",
    yes: 45,
    no: 55,
  },
  {
    id: "squirrel-pizza",
    question: "Will a squirrel steal pizza at the park this weekend?",
    icon: "🐿️",
    category: "Outdoors",
    yes: 30,
    no: 70,
  },
  {
    id: "coffee-cold",
    question: "Will my coffee get cold before the meeting ends today?",
    icon: "☕",
    category: "Office",
    yes: 64,
    no: 36,
  },
  {
    id: "microwave-sauce",
    question: "Will the office microwave become a crime scene this week?",
    icon: "🍝",
    category: "Office",
    yes: 58,
    no: 42,
  },
  {
    id: "wifi-drop",
    question: "Will Wi‑Fi drop during the important presentation?",
    icon: "📶",
    category: "Tech",
    yes: 51,
    no: 49,
  },
  {
    id: "printer-jam",
    question: "Will the printer jam while the boss is watching?",
    icon: "🖨️",
    category: "Office",
    yes: 72,
    no: 28,
  },
  {
    id: "seagull-lunch",
    question: "Will a seagull steal someone’s fries this weekend?",
    icon: "🦅",
    category: "Outdoors",
    yes: 61,
    no: 39,
  },
  {
    id: "cat-zoom",
    question: "Will my cat cameo on a video call this week?",
    icon: "🐈",
    category: "Home",
    yes: 67,
    no: 33,
  },
] as const;

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
    return "❓";
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
