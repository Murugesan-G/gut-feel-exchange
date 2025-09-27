"use client";

import { Button } from "@/components/ui/button";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";

type NewPredictionDialogProps = {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  onAdd: (q: string, icon: string, category: string) => Promise<void> | void;
  iconChoices: string[];
  categoryChoices: string[];
  isMutating?: boolean;
};

export function NewPredictionDialog({ open, onOpenChange, onAdd, iconChoices, categoryChoices, isMutating }: NewPredictionDialogProps) {
  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="brutal-card brutal-modal no-shadow p-0 !gap-0 rounded-none overflow-hidden border-4 border-black">
        <DialogHeader className="brutal-modal-header">
          <DialogTitle className="text-2xl font-black">New Prediction</DialogTitle>
        </DialogHeader>
        <form
          className="brutal-form brutal-form--plain flex flex-col gap-4 px-4 pb-4"
          autoComplete="off"
          onSubmit={async (e) => {
            e.preventDefault();
            const form = e.currentTarget;
            const data = new FormData(form);
            const q = String(data.get("q") || "").trim();
            const icon = String(data.get("icon") || "").trim();
            const cat = String(data.get("cat") || "").trim();
            if (!q) return;
            try {
              await onAdd(q, icon, cat);
              form.reset();
            } catch {
              // handled upstream
            }
          }}
        >
          <div className="flex flex-col gap-1">
            <label className="brutal-label" htmlFor="new-question">
              Prediction
            </label>
            <input
              id="new-question"
              name="q"
              placeholder="Describe the future event"
              className="brutal-field brutal-field--no-caps w-full text-base font-black"
              autoFocus
              autoComplete="off"
              required
              maxLength={100}
              disabled={isMutating}
            />
          </div>
          <div className="grid grid-cols-1 gap-3 sm:grid-cols-2">
            <div className="flex flex-col gap-1">
              <label className="brutal-label" htmlFor="icon-select">
                Symbol
              </label>
              <div className="brutal-select w-full">
                <select
                  id="icon-select"
                  name="icon"
                  className="brutal-field appearance-none w-full pr-16 text-base font-black"
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
                  className="brutal-field appearance-none w-full pr-16 text-base font-black"
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
            <Button
              className="brutal-btn brutal-action bg-lime-300 px-4 py-3 font-black uppercase tracking-[0.2em] h-auto rounded-none border-4 border-black shadow-[4px_4px_0_#000]"
              type="submit"
              disabled={isMutating}
            >
              Add
            </Button>
            <Button
              className="brutal-btn brutal-action bg-red-300 px-4 py-3 font-black uppercase tracking-[0.2em] h-auto rounded-none border-4 border-black shadow-[4px_4px_0_#000]"
              type="button"
              onClick={() => onOpenChange(false)}
            >
              Cancel
            </Button>
          </div>
        </form>
      </DialogContent>
    </Dialog>
  );
}

export default NewPredictionDialog;
