"use client";

import { useState } from "react";
import { gql, useMutation, useQuery } from "@apollo/client";
import { GetSelectionsResponse } from "@/types/movie.types";

const CREATE_MOVIE = gql`
  mutation CreateMovie(
    $title: String!
    $releaseDate: DateTime!
    $budget: Int
    $description: String!
    $popularity: Decimal
    $runtime: Int
    $movieStatus: String!
    $voteAverage: Decimal
    $voteCount: Int
    $pegi: String!
    $tags: [Int!]!
    $genres: [Int!]!
    $movieCasts: [MovieCastInput!]!
    $productionCompany: ProductionCompanyInput!
    $language: LanguageInput!
    $country: CountryInput!
  ) {
    createMovie(
      movie: {
        movieId: 0
        title: $title
        releaseDate: $releaseDate
        budget: $budget
        description: $description
        popularity: $popularity
        runtime: $runtime
        movieStatus: $movieStatus
        voteAverage: $voteAverage
        voteCount: $voteCount
        pegi: $pegi
        tags: $tags
        genre: $genres
        movieCasts: $movieCasts
        productionCompany: $productionCompany
        language: $language
        country: $country
      }
    ) {
      movieId
      title
    }
  }
`;

const GET_SELECTIONS = gql`
  query GetSelections {
    countries {
      countryId
      countryName
    }
    languages {
      languageId
      languageName
    }
    productionCompanies {
      companyId
      companyName
    }
    tags {
      tagId
      tagName
    }
    genres {
      genreId
      genreName
    }
    people {
      personId
      personName
    }
  }
`;

export default function Page() {
  const [form, setForm] = useState({
    title: "",
    releaseDate: "",
    budget: "",
    description: "",
    popularity: "",
    runtime: "",
    movieStatus: "",
    voteAverage: "",
    voteCount: "",
    pegi: "",
    tags: [],
    genres: [],
    movieCasts: [],
    productionCompany: "",
    language: "",
    country: "",
  });

  const { data } = useQuery<GetSelectionsResponse>(GET_SELECTIONS);
  const [createMovie, { loading: creating, error }] = useMutation(CREATE_MOVIE);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    const selectedOptions = (e.target as HTMLSelectElement).selectedOptions;

    if (selectedOptions) {
      const selectedValues = Array.from(selectedOptions, (option) => option.value);
      setForm((prev) => ({
        ...prev,
        [name]: selectedValues,
      }));
    } else {
      setForm((prev) => ({
        ...prev,
        [name]: value,
      }));
    }
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      const formattedReleaseDate = new Date(form.releaseDate + "T00:00:00Z").toISOString();

      // Обновляем поля для мутации, преобразуем к нужным типам данных
      const movieInput = {
        ...form,
        tags: form.tags.map(Number),
        genres: form.genres.map(Number),
        productionCompany: { companyId: Number(form.productionCompany) },
        language: { languageId: Number(form.language) },
        country: { countryId: Number(form.country) },
        releaseDate: formattedReleaseDate,
      };

      await createMovie({
        variables: movieInput,
      });

      setForm({
        title: "",
        releaseDate: "",
        budget: "",
        description: "",
        popularity: "",
        runtime: "",
        movieStatus: "",
        voteAverage: "",
        voteCount: "",
        pegi: "",
        tags: [],
        genres: [],
        movieCasts: [],
        productionCompany: "",
        language: "",
        country: "",
      });
    } catch (err) {
      console.error("Error creating movie:", err);
    }
  };

  return (
    <div className="p-4 max-w-md mx-auto">
      <h1 className="text-xl font-bold mb-4">Create a New Movie</h1>
      <form
        onSubmit={handleSubmit}
        className="flex flex-col gap-2">
        <input
          type="text"
          name="title"
          value={form.title}
          onChange={handleChange}
          placeholder="Title"
          className="p-2 border rounded"
        />
        <input
          type="date"
          name="releaseDate"
          value={form.releaseDate}
          onChange={handleChange}
          className="p-2 border rounded"
        />
        <input
          type="number"
          name="budget"
          value={form.budget}
          onChange={handleChange}
          placeholder="Budget"
          className="p-2 border rounded"
        />
        <textarea
          name="description"
          value={form.description}
          onChange={handleChange}
          placeholder="Description"
          className="p-2 border rounded"
        />
        <input
          type="number"
          name="popularity"
          value={form.popularity}
          onChange={handleChange}
          placeholder="Popularity"
          className="p-2 border rounded"
        />
        <input
          type="number"
          name="runtime"
          value={form.runtime}
          onChange={handleChange}
          placeholder="Runtime"
          className="p-2 border rounded"
        />
        <input
          type="text"
          name="movieStatus"
          value={form.movieStatus}
          onChange={handleChange}
          placeholder="Status"
          className="p-2 border rounded"
        />
        <input
          type="number"
          name="voteAverage"
          value={form.voteAverage}
          onChange={handleChange}
          placeholder="Vote Average"
          className="p-2 border rounded"
        />
        <input
          type="number"
          name="voteCount"
          value={form.voteCount}
          onChange={handleChange}
          placeholder="Vote Count"
          className="p-2 border rounded"
        />
        <input
          type="text"
          name="pegi"
          value={form.pegi}
          onChange={handleChange}
          placeholder="Pegi"
          className="p-2 border rounded"
        />
        <select
          name="country"
          value={form.country}
          onChange={handleChange}
          className="p-2 border rounded">
          <option value="">Select Country</option>
          {data?.countries.map((c) => (
            <option
              key={c.countryId}
              value={c.countryId}>
              {c.countryName}
            </option>
          ))}
        </select>
        <select
          name="productionCompany"
          value={form.productionCompany}
          onChange={handleChange}
          className="p-2 border rounded">
          <option value="">Select Production Company</option>
          {data?.productionCompanies.map((c) => (
            <option
              key={c.companyId}
              value={c.companyId}>
              {c.companyName}
            </option>
          ))}
        </select>
        <select
          name="language"
          value={form.language}
          onChange={handleChange}
          className="p-2 border rounded">
          <option value="">Select Language</option>
          {data?.languages.map((l) => (
            <option
              key={l.languageId}
              value={l.languageId}>
              {l.languageName}
            </option>
          ))}
        </select>
        <select
          name="genres"
          value={form.genres}
          onChange={handleChange}
          className="p-2 border rounded"
          multiple>
          <option value="">Select Genres</option>
          {data?.genres.map((g) => (
            <option
              key={g.genreId}
              value={g.genreId}>
              {g.genreName}
            </option>
          ))}
        </select>
        <select
          name="tags"
          value={form.tags}
          onChange={handleChange}
          className="p-2 border rounded"
          multiple>
          <option value="">Select Tags</option>
          {data?.tags.map((t) => (
            <option
              key={t.tagId}
              value={t.tagId}>
              {t.tagName}
            </option>
          ))}
        </select>
        <button
          type="submit"
          disabled={creating}
          className="bg-blue-500 text-white px-4 py-2 rounded disabled:opacity-50">
          {creating ? "Creating..." : "Create Movie"}
        </button>
      </form>
      {error && <p className="text-red-500 mt-2">Error: {error.message}</p>}
    </div>
  );
}
