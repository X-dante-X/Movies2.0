"use client";

import { twMerge } from "tailwind-merge";
import { filtersData, TFilter } from "./filters.data";
import { useFilterStore } from "@/stores/filter.store";

interface FiltersProps {
  onChange: () => void;
}

export function Filters({ onChange }: FiltersProps) {
  const { currentFilter, setCurrentFilter } = useFilterStore();

  const handleFilterChange = (filter: TFilter) => {
    setCurrentFilter(filter);
    onChange();
  };

  return (
    <div className="text-center mt-10 gap-3 border border-yellow-400 w-max mx-auto rounded">
      {filtersData.map((filter) => (
        <button
          key={filter}
          className={twMerge(
            "px-4 py-2 rounded font-medium transition-colors group hover:text-primary bg-transparent text-white",
            filter === currentFilter && "bg-yellow-400"
          )}
          type="button"
          onClick={() => handleFilterChange(filter)}>
          {filter}
        </button>
      ))}
    </div>
  );
}
