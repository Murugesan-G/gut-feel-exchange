"use client";

import type { Prediction } from "@/types/prediction";
import { PredictionCard } from "./prediction-card";

type PredictionListProps = {
  items: Prediction[];
  isMutating?: boolean;
  onStartVote: (id: string, side: "yes" | "no") => void;
};

export function PredictionList({ items, isMutating, onStartVote }: PredictionListProps) {
  if (items.length === 0) return null;
  return (
    <div className="flex flex-col gap-4">
      {items.map((p) => (
        <PredictionCard
          key={p.id}
          prediction={p}
          isMutating={isMutating}
          onStartVote={onStartVote}
        />
      ))}
    </div>
  );
}

export default PredictionList;
