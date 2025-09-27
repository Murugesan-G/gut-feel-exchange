"use client";

import { AppHeader } from "@/components/app-header";
import { NewPredictionDialog } from "@/components/new-prediction-dialog";
import { PredictionList } from "@/components/prediction-list";
import { StakePickerDialog } from "@/components/stake-picker-dialog";
import { createPrediction, fetchPredictions, voteOnPrediction } from "@/lib/api";
import {
  CATEGORY_ALL_LABEL,
  CATEGORY_CHOICES,
  DEFAULT_CATEGORY,
  ICON_CHOICES,
} from "@/lib/constants";
import type { Prediction } from "@/types/prediction";
import { useCallback, useEffect, useMemo, useState } from "react";

export default function Home() {
  const [predictions, setPredictions] = useState<Prediction[]>([]);
  const fallbackCategory = DEFAULT_CATEGORY;
  const defaultCategory = CATEGORY_ALL_LABEL;
  const [activeCat, setActiveCat] = useState<string>(defaultCategory);
  const [showNew, setShowNew] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [isMutating, setIsMutating] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [pendingVote, setPendingVote] = useState<{ id: string; side: "yes" | "no" } | null>(
    null,
  );

  useEffect(() => {
    const controller = new AbortController();
    let isCancelled = false;

    const loadPredictions = async () => {
      setIsLoading(true);
      setError(null);

      try {
        const items = await fetchPredictions(controller.signal);
        if (!isCancelled) {
          setPredictions(items);
        }
      } catch (err) {
        if ((err as DOMException | null)?.name === "AbortError") {
          return;
        }
        console.error(err);
        if (!isCancelled) {
          setError("Could not load predictions. Please try again.");
        }
      } finally {
        if (!isCancelled) {
          setIsLoading(false);
        }
      }
    };

    void loadPredictions();

    return () => {
      isCancelled = true;
      controller.abort();
    };
  }, []);

  const cats = useMemo(() => {
    return [defaultCategory, ...CATEGORY_CHOICES];
  }, [defaultCategory]);

  const filtered = useMemo(() => {
    const byPopularity = (a: Prediction, b: Prediction) =>
      b.yes + b.no - (a.yes + a.no);

    if (activeCat === defaultCategory) {
      return [...predictions].sort(byPopularity);
    }
    return predictions.filter((p) => p.category === activeCat).sort(byPopularity);
  }, [predictions, activeCat, defaultCategory]);

  const iconChoices = useMemo(() => Array.from(ICON_CHOICES), []);

  const categoryChoices = useMemo(() => {
    const list = CATEGORY_CHOICES.filter((cat) => cat !== fallbackCategory);
    return [fallbackCategory, ...list];
  }, [fallbackCategory]);

  const handleStartVote = useCallback(
    (id: string, side: "yes" | "no") => setPendingVote({ id, side }),
    [],
  );

  async function vote(id: string, side: "yes" | "no", stake: number) {
    try {
      setIsMutating(true);
      const next = await voteOnPrediction(id, { side, stake });
      setPredictions(next);
      setError(null);
    } catch (err) {
      console.error(err);
      setError("Voting failed. Please retry.");
      throw err;
    } finally {
      setIsMutating(false);
    }
  }

  async function addPrediction(q: string, icon: string, category: string) {
    if (!q.trim()) return;
    try {
      setIsMutating(true);
      const next = await createPrediction({ question: q, icon, category });
      setPredictions(next);
      setError(null);
      setShowNew(false);
    } catch (err) {
      console.error(err);
      setError("Adding prediction failed. Please retry.");
      throw err;
    } finally {
      setIsMutating(false);
    }
  }

  return (
    <div className="brutal-container min-h-dvh p-4">
      <AppHeader
        onAdd={() => setShowNew(true)}
        disabled={isMutating}
        categories={cats}
        value={activeCat}
        onChange={setActiveCat}
      />

      {error && (
        <div className="mt-4 brutal-card bg-red-100 p-3 font-bold text-sm">{error}</div>
      )}

      <main className="mt-4 flex flex-col gap-4">
        {isLoading && (
          <article className="brutal-card p-4 text-sm font-bold">Loading predictions...</article>
        )}
        {!isLoading && filtered.length === 0 && (
          <article className="brutal-card p-4 text-sm font-bold">No predictions yet. Add one to get started.</article>
        )}
        <PredictionList
          items={filtered}
          isMutating={isMutating}
          onStartVote={handleStartVote}
        />
      </main>

      <NewPredictionDialog
        open={showNew}
        onOpenChange={setShowNew}
        onAdd={addPrediction}
        iconChoices={iconChoices}
        categoryChoices={categoryChoices}
        isMutating={isMutating}
      />

      <StakePickerDialog
        open={Boolean(pendingVote)}
        pending={pendingVote}
        isMutating={isMutating}
        onOpenChange={(open) => {
          if (!open) setPendingVote(null);
        }}
        onConfirm={async (stake) => {
          if (!pendingVote) return;
          try {
            await vote(pendingVote.id, pendingVote.side, stake);
            setPendingVote(null);
          } catch {
            // handled in vote()
          }
        }}
      />
    </div>
  );
}
