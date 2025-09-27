"use client";

import type { Prediction } from "@/types/prediction";

type PredictionCardProps = {
  prediction: Prediction;
  isMutating?: boolean;
  onStartVote: (id: string, side: "yes" | "no") => void;
};

export function PredictionCard({ prediction: p, isMutating, onStartVote }: PredictionCardProps) {
  const total = p.yes + p.no || 1;
  const yesPct = Math.round((p.yes / total) * 100);
  const noPct = 100 - yesPct;

  return (
    <article className="brutal-card p-4">
      <div className="flex gap-3 items-start">
        <div className="flex h-12 w-12 items-center justify-center border-4 border-black text-2xl bg-lime-200">
          <span aria-hidden>{p.icon}</span>
        </div>
        <div className="flex-1">
          <h2 className="text-lg font-extrabold leading-snug">{p.question}</h2>
          <div className="mt-2 h-3 w-full bg-white border-2 border-black">
            <div className="h-full bg-orange-400" style={{ width: `${yesPct}%` }} />
          </div>
          <div className="mt-3 grid grid-cols-2 gap-2 text-sm font-extrabold">
            <button
              className="brutal-btn bg-orange-300 px-3 py-3"
              onClick={() => onStartVote(p.id, "yes")}
              disabled={isMutating}
            >
              {yesPct}% YES
            </button>
            <button
              className="brutal-btn bg-purple-300 px-3 py-3"
              onClick={() => onStartVote(p.id, "no")}
              disabled={isMutating}
            >
              {noPct}% NO
            </button>
          </div>
        </div>
      </div>
    </article>
  );
}

export default PredictionCard;
