// Shared app constants
export const DEFAULT_STAKE = 10;
export const PREDICTIONS_KV_KEY = "predictions/all";

// Domain limits and defaults
export const MAX_QUESTION_LENGTH = 100;
export const MAX_CATEGORY_LENGTH = 32;
export const DEFAULT_NEW_VOTE = 50;
export const MAX_PREDICTIONS = 500;
export const DEFAULT_CATEGORY = "Tech";
export const DEFAULT_ICON = "❓";

// Predefined selections shared across client and server
export const ICON_CHOICES = [
  "❓",
  "🤖",
  "🚀",
  "📈",
  "🎯",
  "💡",
  "🛰️",
  "🧠",
  "📊",
  "🏆",
  "🌧️",
  "🔥",
  "🎉",
  "🥔",
  "🐿️",
  "☕",
  "🍝",
  "📶",
  "🖨️",
  "🦅",
  "🐈",
] as const;

export const CATEGORY_CHOICES = [
  "Tech",
  "Office",
  "Food",
  "Outdoors",
  "Home",
  "Sports",
  "Entertainment",
  "Weather",
  "Finance",
  "Science",
  "Travel",
  "Culture",
] as const;

export type IconChoice = (typeof ICON_CHOICES)[number];
export type CategoryChoice = (typeof CATEGORY_CHOICES)[number];

// UI constants
export const CATEGORY_ALL_LABEL = "All";
export const STAKE_PRESET_OPTIONS = [1, 10, 100] as const;

// Common response headers for JSON APIs
export const JSON_HEADERS = {
  "Content-Type": "application/json; charset=utf-8",
  "Cache-Control": "no-store",
} as const;

// Seed data used to initialize the store
export const SEED_SOURCE = [
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
