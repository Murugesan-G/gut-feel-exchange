export type Prediction = {
  id: string;
  question: string;
  icon: string;
  category: string;
  yes: number;
  no: number;
  createdAt: number;
  updatedAt: number;
};

export type CreatePredictionInput = {
  question: string;
  icon: string;
  category: string;
};

export type VoteInput = {
  id: string;
  side: "yes" | "no";
  stake: number;
};

export type PredictionStore = {
  version: string;
  predictions: Prediction[];
};

// API shapes
export type PredictionsResponse = { predictions: Prediction[] };
export type VoteBody = Omit<VoteInput, "id">;

// App/server constants
export const DEFAULT_STAKE = 10;
export const PREDICTIONS_KV_KEY = "predictions/all";
