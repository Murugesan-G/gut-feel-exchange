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

// API shapes
export type PredictionsResponse = { predictions: Prediction[] };
export type VoteBody = {
  side: "yes" | "no";
  stake: number;
};
