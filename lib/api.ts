import type { CreatePredictionInput, Prediction, PredictionsResponse, VoteBody } from "@/types/prediction";

export async function fetchPredictions(signal?: AbortSignal): Promise<Prediction[]> {
  const res = await fetch("/api/predictions", {
    method: "GET",
    headers: { Accept: "application/json" },
    cache: "no-store",
    signal,
  });
  if (!res.ok) {
    throw new Error(`Failed to load predictions: ${res.status}`);
  }
  const data = (await res.json()) as PredictionsResponse;
  return data.predictions;
}

export async function createPrediction(body: CreatePredictionInput): Promise<Prediction[]> {
  const res = await fetch("/api/predictions", {
    method: "POST",
    headers: { "Content-Type": "application/json", Accept: "application/json" },
    body: JSON.stringify(body),
  });
  if (!res.ok) {
    throw new Error(`Failed to add prediction: ${res.status}`);
  }
  const data = (await res.json()) as PredictionsResponse;
  return data.predictions;
}

export async function voteOnPrediction(id: string, body: VoteBody): Promise<Prediction[]> {
  const res = await fetch(`/api/predictions/${id}/vote`, {
    method: "POST",
    headers: { "Content-Type": "application/json", Accept: "application/json" },
    body: JSON.stringify(body),
  });
  if (!res.ok) {
    throw new Error(`Failed to submit vote: ${res.status}`);
  }
  const data = (await res.json()) as PredictionsResponse;
  return data.predictions;
}
