"use client";

import { Button } from "@/components/ui/button";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { DEFAULT_STAKE, STAKE_PRESET_OPTIONS } from "@/lib/constants";
import { useEffect, useRef, useState } from "react";

type PendingVote = { id: string; side: "yes" | "no" } | null;

type StakePickerDialogProps = {
  open: boolean;
  pending: PendingVote;
  isMutating?: boolean;
  onOpenChange: (open: boolean) => void;
  onConfirm: (stake: number) => Promise<void> | void;
};

export function StakePickerDialog({ open, pending, isMutating, onOpenChange, onConfirm }: StakePickerDialogProps) {
  const [stake, setStake] = useState<number>(DEFAULT_STAKE);
  const customInputRef = useRef<HTMLInputElement | null>(null);

  // When dialog opens for a new vote, always reset to the default stake
  useEffect(() => {
    if (open && pending) setStake(DEFAULT_STAKE);
  }, [open, pending]);

  const changeStake = (value: number) => {
    const next = Math.max(1, value);
    setStake(next);
  };

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="brutal-card brutal-modal no-shadow bg-sky-100 p-0 !gap-0 rounded-none overflow-hidden border-4 border-black">
        <DialogHeader className="brutal-modal-header">
          <DialogTitle className="text-2xl font-black">Choose Stake</DialogTitle>
        </DialogHeader>
        <div className="brutal-form brutal-form--plain flex flex-col gap-4 px-4 pb-4">
          <div className="grid grid-cols-2 sm:grid-cols-4 gap-3 pt-2">
            {STAKE_PRESET_OPTIONS.map((v) => (
              <button
                key={v}
                type="button"
                className={`brutal-field brutal-choice text-base font-black ${
                  stake === v ? "is-active" : ""
                }`}
                onClick={() => changeStake(v)}
                disabled={isMutating}
              >
                ${v}
              </button>
            ))}
            <div
              role="group"
              className={`brutal-field brutal-choice relative p-0 text-base font-black ${
                !STAKE_PRESET_OPTIONS.includes(
                  stake as (typeof STAKE_PRESET_OPTIONS)[number]
                )
                  ? "is-active"
                  : ""
              }`}
              onClick={() => customInputRef.current?.focus()}
            >
              <span
                aria-hidden
                className="pointer-events-none absolute left-2 top-1/2 -translate-y-1/2 select-none text-base"
              >
                $
              </span>
              <input
                ref={customInputRef}
                id="custom-stake"
                type="number"
                min={1}
                inputMode="numeric"
                pattern="[0-9]*"
                className="block w-full bg-transparent pl-3 pr-1 py-2 text-left font-black text-base leading-none outline-none border-0 brutal-field--no-caps tracking-[0] [appearance:textfield] [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:appearance-none [font-variant-numeric:tabular-nums]"
                value={stake}
                onChange={(e) => changeStake(Number(e.target.value || 1))}
                autoComplete="off"
                aria-label="Custom stake"
                onKeyDown={(e) => e.stopPropagation()}
                onPointerDown={(e) => e.stopPropagation()}
                disabled={isMutating}
              />
            </div>
          </div>
          <div className="grid grid-cols-1 gap-3 sm:grid-cols-2 brutal-form-actions">
            <Button
              className="brutal-btn brutal-action bg-lime-300 px-4 py-3 font-black uppercase tracking-[0.2em] h-auto rounded-none border-4 border-black shadow-[4px_4px_0_#000]"
              type="button"
              disabled={isMutating}
              onClick={() => void onConfirm(stake)}
            >
              Vote ${stake}
            </Button>
            <Button
              className="brutal-btn brutal-action bg-red-300 px-4 py-3 font-black uppercase tracking-[0.2em] h-auto rounded-none border-4 border-black shadow-[4px_4px_0_#000]"
              type="button"
              onClick={() => onOpenChange(false)}
            >
              Cancel
            </Button>
          </div>
        </div>
      </DialogContent>
    </Dialog>
  );
}

export default StakePickerDialog;
