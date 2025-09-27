"use client";

type CategoryFilterProps = {
  categories: string[];
  value: string;
  onChange: (value: string) => void;
};

export function CategoryFilter({ categories, value, onChange }: CategoryFilterProps) {
  return (
    <nav className="mt-3 flex gap-3 overflow-x-auto no-scrollbar text-sm">
      {categories.map((c) => (
        <button
          key={c}
          onClick={() => onChange(c)}
          className={`px-2 py-1 font-bold border-b-4 ${value === c ? "border-black" : "border-transparent"}`}
        >
          {c}
        </button>
      ))}
    </nav>
  );
}

export default CategoryFilter;
