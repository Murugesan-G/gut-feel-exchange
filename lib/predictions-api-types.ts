import type { NewPredictionInput, Prediction, VoteInput } from "@/lib/predictions";

export type PredictionsResponse = {
  predictions: Prediction[];
};

export type CreatePredictionBody = NewPredictionInput;

export type VoteBody = Omit<VoteInput, "id">;
