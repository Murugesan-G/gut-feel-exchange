"use client";

import { useEffect, useMemo, useState } from "react";
import { DEFAULT_STAKE, Prediction } from "@/lib/predictions";

type PredictionsResponse = {
  predictions: Prediction[];
};

type VotePayload = {
  side: "yes" | "no";
  stake: number;
};

type CreatePayload = {
  question: string;
  icon: string;
  category: string;
};

async function apiGetPredictions(signal?: AbortSignal): Promise<Prediction[]> {
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

async function apiCreatePrediction(body: CreatePayload): Promise<Prediction[]> {
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

async function apiVotePrediction(id: string, body: VotePayload): Promise<Prediction[]> {
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

export default function Home() {
  const [predictions, setPredictions] = useState<Prediction[]>([]);
  const defaultCategory = "All";
  const [activeCat, setActiveCat] = useState<string>(defaultCategory);
  const [lastStake, setLastStake] = useState<number>(DEFAULT_STAKE);
  const [showNew, setShowNew] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [isMutating, setIsMutating] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [pendingVote, setPendingVote] = useState<
    | { id: string; side: "yes" | "no"; tempStake: number }
    | null
  >(null);

  useEffect(() => {
    const controller = new AbortController();
    setIsLoading(true);
    setError(null);

    void apiGetPredictions(controller.signal)
      .then((items) => {
        setPredictions(items);
      })
      .catch((err) => {
        console.error(err);
        setError("Could not load predictions. Please try again.");
      })
      .finally(() => {
        setIsLoading(false);
      });

    // No client persistence: initialize to default stake each session
    setLastStake(DEFAULT_STAKE);

    return () => controller.abort();
  }, []);

  const cats = useMemo(() => {
    const set = new Set<string>([defaultCategory]);
    predictions.forEach((p) => set.add(p.category));
    return Array.from(set);
  }, [predictions]);

  const filtered = useMemo(() => {
    if (activeCat === defaultCategory) {
      return [...predictions].sort((a, b) => b.yes + b.no - (a.yes + a.no));
    }
    return predictions.filter((p) => p.category === activeCat);
  }, [predictions, activeCat]);

  const iconChoices = useMemo(() => {
    const set = new Set<string>();
    predictions.forEach((p) => set.add(p.icon));
    return Array.from(set);
  }, [predictions]);

  const categoryChoices = useMemo(() => {
    return cats.filter((c) => c !== defaultCategory);
  }, [cats]);

  async function vote(id: string, side: "yes" | "no", stake: number) {
    try {
      setIsMutating(true);
      const next = await apiVotePrediction(id, { side, stake });
      setPredictions(next);
      setLastStake(stake);
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
      const next = await apiCreatePrediction({ question: q, icon, category });
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
    <div className="brutal-container min-h-dvh p-4 pb-24">
      <header className="sticky top-0 z-10 bg-white brutal-card px-4 py-3">
        <div className="flex items-center justify-between">
          <h1 className="text-2xl font-extrabold tracking-tight">PREDICTIONS</h1>
          <button
            className="brutal-btn bg-lime-300 px-3 py-2 font-bold"
            onClick={() => setShowNew(true)}
            aria-label="Add prediction"
            disabled={isMutating}
          >
            +
          </button>
        </div>
        <nav className="mt-3 flex gap-3 overflow-x-auto text-sm">
          {cats.map((c) => (
            <button
              key={c}
              onClick={() => setActiveCat(c)}
              className={`nav-pill px-2 py-1 font-bold ${
                activeCat === c ? "active" : ""
              }`}
            >
              {c}
            </button>
          ))}
        </nav>
      </header>

      {error && (
        <div className="mt-4 brutal-card bg-red-100 p-3 font-bold text-sm">
          {error}
        </div>
      )}

      <main className="mt-4 flex flex-col gap-4 pb-24">
        {isLoading && (
          <article className="brutal-card p-4 text-sm font-bold">
            Loading predictions...
          </article>
        )}

        {!isLoading && filtered.length === 0 && (
          <article className="brutal-card p-4 text-sm font-bold">
            No predictions yet. Add one to get started.
          </article>
        )}

        {filtered.map((p) => {
          const total = p.yes + p.no || 1;
          const yesPct = Math.round((p.yes / total) * 100);
          const noPct = 100 - yesPct;
          return (
            <article key={p.id} className="brutal-card p-4">
              <div className="flex gap-3 items-start">
                <div className="flex h-12 w-12 items-center justify-center border-4 border-black text-2xl bg-lime-200">
                  <span aria-hidden>{p.icon}</span>
                </div>
                <div className="flex-1">
                  <h2 className="text-lg font-extrabold leading-snug">
                    {p.question}
                  </h2>
                  <div className="mt-2 progress-track h-3 w-full bg-white">
                    <div
                      className="h-full bg-orange-400"
                      style={{ width: `${yesPct}%` }}
                    />
                  </div>
                  <div className="mt-3 grid grid-cols-2 gap-2 text-sm font-extrabold">
                    <button
                      className="brutal-btn bg-orange-300 px-3 py-3"
                      onClick={() =>
                        setPendingVote({ id: p.id, side: "yes", tempStake: lastStake })
                      }
                      disabled={isMutating}
                    >
                      {yesPct}% YES
                    </button>
                    <button
                      className="brutal-btn bg-purple-300 px-3 py-3"
                      onClick={() =>
                        setPendingVote({ id: p.id, side: "no", tempStake: lastStake })
                      }
                      disabled={isMutating}
                    >
                      {noPct}% NO
                    </button>
                  </div>
                </div>
              </div>
            </article>
          );
        })}
      </main>

      {showNew && (
        <div className="fixed inset-0 z-20 bg-black/40 flex items-end">
          <div className="brutal-container w-full px-4 pb-0">
            <div className="brutal-card brutal-modal no-shadow bg-yellow-100 p-0 rounded-none overflow-hidden">
              <header className="brutal-modal-header">
                <h3 className="text-2xl font-black">New Prediction</h3>
              </header>
              <form
                className="brutal-form brutal-form--plain flex flex-col gap-4"
                onSubmit={async (e) => {
                  e.preventDefault();
                  const form = e.currentTarget;
                  const data = new FormData(form);
                  const q = String(data.get("q") || "").trim();
                  const icon = String(data.get("icon") || "").trim();
                  const cat = String(data.get("cat") || "").trim();
                  if (!q) return;
                  try {
                    await addPrediction(q, icon, cat);
                    form.reset();
                  } catch {
                    // handled in addPrediction
                  }
                }}
              >
                <div className="flex flex-col gap-1">
                  <label className="brutal-label brutal-label--no-caps" htmlFor="new-question">
                    Prediction
                  </label>
                  <input
                    id="new-question"
                    name="q"
                    placeholder="Describe the future event"
                    className="brutal-field brutal-field--no-caps w-full"
                    autoFocus
                    required
                    maxLength={100}
                    disabled={isMutating}
                  />
                </div>
                <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
                  <div className="flex flex-col gap-1">
                    <label className="brutal-label" htmlFor="icon-select">
                      Symbol
                    </label>
                    <div className="brutal-select w-full">
                      <select
                        id="icon-select"
                        name="icon"
                        className="brutal-field appearance-none w-full pr-16"
                        defaultValue={iconChoices[0] || ""}
                        required
                        disabled={isMutating}
                      >
                        {iconChoices.length === 0 && (
                          <option value="" disabled>
                            Choose a symbol
                          </option>
                        )}
                        {iconChoices.map((ico) => (
                          <option key={ico} value={ico}>
                            {ico}
                          </option>
                        ))}
                      </select>
                      <span aria-hidden className="brutal-select-sep" />
                      <span aria-hidden className="brutal-select-caret" />
                    </div>
                  </div>
                  <div className="flex flex-col gap-1">
                    <label className="brutal-label" htmlFor="cat-select">
                      Category
                    </label>
                    <div className="brutal-select w-full">
                      <select
                        id="cat-select"
                        name="cat"
                        className="brutal-field appearance-none w-full pr-16"
                        defaultValue={categoryChoices[0] || ""}
                        required
                        disabled={isMutating}
                      >
                        {categoryChoices.length === 0 && (
                          <option value="" disabled>
                            Choose a category
                          </option>
                        )}
                        {categoryChoices.map((c) => (
                          <option key={c} value={c}>
                            {c}
                          </option>
                        ))}
                      </select>
                      <span aria-hidden className="brutal-select-sep" />
                      <span aria-hidden className="brutal-select-caret" />
                    </div>
                  </div>
                </div>
                <div className="grid grid-cols-1 gap-3 sm:grid-cols-2 brutal-form-actions">
                  <button
                    className="brutal-btn brutal-action bg-lime-300 px-4 py-3 font-black uppercase tracking-[0.2em]"
                    type="submit"
                    disabled={isMutating}
                  >
                    Add
                  </button>
                  <button
                    className="brutal-btn brutal-action bg-red-300 px-4 py-3 font-black uppercase tracking-[0.2em]"
                    type="button"
                    onClick={() => setShowNew(false)}
                  >
                    Cancel
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      )}

      {pendingVote && (
        <div className="fixed inset-0 z-30 bg-black/40 flex items-end">
          <div className="brutal-container w-full px-4 pb-0">
            <div className="brutal-card brutal-modal no-shadow bg-sky-100 p-0 rounded-none overflow-hidden">
              <header className="brutal-modal-header">
                <h3 className="text-2xl font-black">Choose Stake</h3>
              </header>
              <div className="brutal-form brutal-form--plain flex flex-col gap-4">
                <div className="grid grid-cols-2 sm:grid-cols-4 gap-3 brutal-field-group">
                  {[1, 10, 100].map((v) => (
                    <button
                      key={v}
                      type="button"
                      className={`brutal-field brutal-choice text-base font-black ${
                        pendingVote.tempStake === v ? "is-active" : ""
                      }`}
                      onClick={() => setPendingVote({ ...pendingVote, tempStake: v })}
                      disabled={isMutating}
                    >
                      ${v}
                    </button>
                  ))}
                  <input
                    id="custom-stake"
                    type="number"
                    min={1}
                    className={`brutal-field brutal-choice text-base font-black ${
                      ![1, 10, 100].includes(pendingVote.tempStake) ? "is-active" : ""
                    }`}
                    value={pendingVote.tempStake}
                    onChange={(e) =>
                      setPendingVote({
                        ...pendingVote,
                        tempStake: Math.max(1, Number(e.target.value || 1)),
                      })
                    }
                    disabled={isMutating}
                  />
                </div>
                <div className="grid grid-cols-1 gap-3 sm:grid-cols-2 brutal-form-actions">
                  <button
                    className="brutal-btn brutal-action bg-lime-300 px-4 py-3 font-black uppercase tracking-[0.2em]"
                    type="button"
                    disabled={isMutating}
                    onClick={async () => {
                      try {
                        await vote(pendingVote.id, pendingVote.side, pendingVote.tempStake);
                        setPendingVote(null);
                      } catch {
                        // handled in vote()
                      }
                    }}
                  >
                    Vote ${pendingVote.tempStake}
                  </button>
                  <button
                    className="brutal-btn brutal-action bg-red-300 px-4 py-3 font-black uppercase tracking-[0.2em]"
                    type="button"
                    onClick={() => setPendingVote(null)}
                  >
                    Cancel
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
