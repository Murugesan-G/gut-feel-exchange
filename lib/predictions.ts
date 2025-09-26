export type Prediction = {
  id: string;
  question: string;
  icon: string;
  category: string;
  yes: number;
  no: number;
  createdAt: number;
};

export type NewPredictionInput = {
  question: string;
  icon: string;
  category: string;
};

export type VoteInput = {
  id: string;
  side: "yes" | "no";
  stake: number;
};

const MAX_QUESTION_LENGTH = 120;
const MAX_CATEGORY_LENGTH = 32;
const DEFAULT_NEW_VOTE = 50;

export const DEFAULT_CATEGORY = "Tech";

const SEED_SOURCE = [
  {
    id: "tater-tots",
    question: "Will school lunch have tater tots next week?",
    icon: "🥔",
    category: "Food",
    yes: 45,
    no: 55,
    hoursAgo: 72,
  },
  {
    id: "squirrel-pizza",
    question: "Will a squirrel steal pizza at the park this weekend?",
    icon: "🐿️",
    category: "Outdoors",
    yes: 30,
    no: 70,
    hoursAgo: 22,
  },
  {
    id: "coffee-cold",
    question: "Will my coffee get cold before the meeting ends today?",
    icon: "☕",
    category: "Office",
    yes: 64,
    no: 36,
    hoursAgo: 10,
  },
  {
    id: "microwave-sauce",
    question: "Will the office microwave become a crime scene this week?",
    icon: "🍝",
    category: "Office",
    yes: 58,
    no: 42,
    hoursAgo: 40,
  },
  {
    id: "wifi-drop",
    question: "Will Wi‑Fi drop during the important presentation?",
    icon: "📶",
    category: "Tech",
    yes: 51,
    no: 49,
    hoursAgo: 5,
  },
  {
    id: "printer-jam",
    question: "Will the printer jam while the boss is watching?",
    icon: "🖨️",
    category: "Office",
    yes: 72,
    no: 28,
    hoursAgo: 70,
  },
  {
    id: "seagull-lunch",
    question: "Will a seagull steal someone’s fries this weekend?",
    icon: "🦅",
    category: "Outdoors",
    yes: 61,
    no: 39,
    hoursAgo: 18,
  },
  {
    id: "cat-zoom",
    question: "Will my cat cameo on a video call this week?",
    icon: "🐈",
    category: "Home",
    yes: 67,
    no: 33,
    hoursAgo: 12,
  },
] as const;

export const DEFAULT_STAKE = 10;
export const PREDICTIONS_KV_KEY = "predictions/all";

export const MAX_PREDICTIONS = 500;

export type PredictionStore = {
  version: string;
  predictions: Prediction[];
};

export function buildSeedPredictions(now: number = Date.now()): Prediction[] {
  return SEED_SOURCE.map((seed) => ({
    ...seed,
    createdAt: now - seed.hoursAgo * 60 * 60 * 1000,
  }));
}

export function createPrediction(
  input: NewPredictionInput,
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
  };
}

function generatePredictionId(): string {
  if (typeof crypto !== "undefined" && typeof crypto.randomUUID === "function") {
    return crypto.randomUUID();
  }

  // Fallback for environments where `crypto.randomUUID` is unavailable.
  return `${Date.now().toString(36)}-${Math.random().toString(36).slice(2)}`;
}

export function sanitizeQuestion(raw: string): string {
  return raw.trim().slice(0, MAX_QUESTION_LENGTH);
}

export function sanitizeIcon(raw: string): string {
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

export function sanitizeCategory(raw: string): string {
  const trimmed = raw.trim();
  if (trimmed.length === 0) {
    return DEFAULT_CATEGORY;
  }
  return trimmed.slice(0, MAX_CATEGORY_LENGTH);
}

export function isPrediction(value: unknown): value is Prediction {
  if (typeof value !== "object" || value === null) {
    return false;
  }
  const data = value as Record<string, unknown>;
  return (
    typeof data.id === "string" &&
    typeof data.question === "string" &&
    typeof data.icon === "string" &&
    typeof data.category === "string" &&
    typeof data.yes === "number" &&
    typeof data.no === "number" &&
    typeof data.createdAt === "number"
  );
}

export function normalizePredictionList(items: unknown): Prediction[] {
  if (!Array.isArray(items)) {
    return [];
  }
  return items.filter(isPrediction).map((item) => ({ ...item }));
}

export function truncatePredictions(predictions: Prediction[]): Prediction[] {
  if (predictions.length <= MAX_PREDICTIONS) {
    return predictions;
  }
  return [...predictions]
    .sort((a, b) => b.createdAt - a.createdAt)
    .slice(0, MAX_PREDICTIONS);
}
