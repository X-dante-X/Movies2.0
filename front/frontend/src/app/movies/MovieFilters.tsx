"use client";
import React from "react";
import { m } from "framer-motion";
import { Genre, Country, ProductionCompany, Tag } from "@/graphql/movie.query";
import { X } from "lucide-react";

export type SelectedFilters = {
  genreIds: number[];
  countryIds: number[];
  companyIds: number[];
  tagIds: number[];
  strictGenres: boolean;
  strictTags: boolean;
};

type Props = {
  filtersData: {
    genres?: Genre[];
    countries?: Country[];
    productionCompanies?: ProductionCompany[];
    tags?: Tag[];
  };
  selectedFilters: SelectedFilters;
  onChange: (filters: SelectedFilters) => void;
  onClose: () => void;
};

const containerVariants = {
  hidden: { opacity: 0, scale: 0.95, x: 50 },
  visible: { opacity: 1, scale: 1, x: 0, transition: { duration: 0.3 } },
  exit: { opacity: 0, scale: 0.95, x: 50, transition: { duration: 0.2 } },
};

export default function MovieFilters({ filtersData, selectedFilters, onChange, onClose }: Props) {
  const { genres = [], countries = [], productionCompanies = [], tags = [] } = filtersData;

  const toggle = (key: keyof SelectedFilters, id: number) => {
    const current = selectedFilters[key] as number[];
    const updated = current?.includes(id) ? current.filter((v) => v !== id) : [...current, id];
    onChange({ ...selectedFilters, [key]: updated });
  };

  const toggleStrict = (key: "strictGenres" | "strictTags") => {
    onChange({ ...selectedFilters, [key]: !selectedFilters[key] });
  };

  return (
    <m.div
      className="fixed top-0 right-0 w-1/3 h-full backdrop-blur-md bg-white/30 border border-white/40 shadow-lg z-50 overflow-y-auto p-6 rounded-xl"
      variants={containerVariants}
      initial="hidden"
      animate="visible"
      exit="exit">
      <button
        onClick={onClose}
        className="absolute top-4 right-4 p-2 rounded-md bg-red-400 text-gray-700 hover:bg-red-500 hover:text-gray-900 transition-colors">
        <X className="w-5 h-5" />
      </button>

      <section className="mb-6 mt-3">
        <h2 className="text-2xl font-bold border-b-indigo-400 border-b-2 text-indigo-200 mb-2">Genres</h2>
        <label className="hidden mt-2 hover:bg-gray-200/20 rounded text-lg font-bold text-indigo-100 mb-2">
          <input
            type="checkbox"
            checked={selectedFilters.strictGenres}
            onChange={() => toggleStrict("strictGenres")}
            className="mr-2"
          />
          Strict genres (all selected genres must be present)
        </label>
        <div className="grid grid-cols-2 gap-4">
          {genres.map((g) => (
            <label
              key={g.genreId}
              className="block text-lg hover:bg-gray-200/20 rounded p-0.5">
              <input
                type="checkbox"
                checked={selectedFilters.genreIds.includes(g.genreId)}
                onChange={() => toggle("genreIds", g.genreId)}
                className="mr-2"
              />
              {g.genreName}
            </label>
          ))}
        </div>
      </section>

      <section className="mb-6">
        <h2 className="text-2xl font-bold border-b-indigo-400 border-b-2 text-indigo-200 mb-2">Tags</h2>
        <label className="hidden mt-2 hover:bg-gray-200/20 rounded text-lg font-bold text-indigo-100 mb-2">
          <input
            type="checkbox"
            checked={selectedFilters.strictTags}
            onChange={() => toggleStrict("strictTags")}
            className="mr-2"
          />
          Strict tags (all selected tags must be present)
        </label>
        <div className="grid grid-cols-2 gap-4">
          {tags.map((t) => (
            <label
              key={t.tagId}
              className="block text-lg hover:bg-gray-200/20 rounded p-0.5">
              <input
                type="checkbox"
                checked={selectedFilters.tagIds.includes(t.tagId)}
                onChange={() => toggle("tagIds", t.tagId)}
                className="mr-2"
              />
              {t.tagName}
            </label>
          ))}
        </div>
      </section>

      <section className="mb-6">
        <h2 className="text-2xl font-bold border-b-indigo-400 border-b-2 text-indigo-200 mb-2">Countries</h2>
        <div className="grid grid-cols-2 gap-4">
          {countries.map((c) => (
            <label
              key={c.countryId}
              className="block text-lg hover:bg-gray-200/20 rounded p-0.5">
              <input
                type="checkbox"
                checked={selectedFilters.countryIds.includes(c.countryId)}
                onChange={() => toggle("countryIds", c.countryId)}
                className="mr-2"
              />
              {c.countryName}
            </label>
          ))}
        </div>
      </section>

      <section className="mb-6">
        <h2 className="text-2xl font-bold border-b-indigo-400 border-b-2 text-indigo-200 mb-2">Companies</h2>
        <div className="grid grid-cols-2 gap-4">
          {productionCompanies.map((c) => (
            <label
              key={c.companyId}
              className="block text-lg hover:bg-gray-200/20 rounded p-0.5">
              <input
                type="checkbox"
                checked={selectedFilters.companyIds.includes(c.companyId)}
                onChange={() => toggle("companyIds", c.companyId)}
                className="mr-2"
              />
              {c.companyName}
            </label>
          ))}
        </div>
      </section>
    </m.div>
  );
}
