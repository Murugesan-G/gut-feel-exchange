"use client";

import { CategoryFilter } from "@/components/category-filter";
import { Button } from "@/components/ui/button";
import { Plus } from "lucide-react";

type AppHeaderProps = {
  onAdd: () => void;
  disabled?: boolean;
  categories: string[];
  value: string;
  onChange: (value: string) => void;
};

export function AppHeader({ onAdd, disabled, categories, value, onChange }: AppHeaderProps) {
  return (
    <header className="sticky top-0 z-10 bg-white brutal-card px-4 py-3">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-extrabold tracking-tight">GUT FEEL EXCHANGE</h1>
        <Button
          type="button"
          className="brutal-btn bg-lime-300 px-3 py-2 font-bold h-auto rounded-none border-4 border-black shadow-[4px_4px_0_#000]"
          onClick={onAdd}
          aria-label="Add prediction"
          disabled={disabled}
          variant="default"
          size="icon"
        >
          <Plus className="size-5" />
        </Button>
      </div>
      <CategoryFilter categories={categories} value={value} onChange={onChange} />
    </header>
  );
}

export default AppHeader;
