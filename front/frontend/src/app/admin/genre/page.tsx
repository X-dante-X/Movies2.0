"use client";

import { useState } from "react";
import { gql, useMutation } from "@apollo/client";

const CREATE_GENRE = gql`
  mutation CreateGenre($genreName: String!) {
    createGenre(genre: { genreId: 0, genreName: $genreName }) {
      genreId
      genreName
    }
  }
`;

export default function Page() {
  const [genreName, setGenreName] = useState("");
  const [createGenre, { data, loading, error }] = useMutation(CREATE_GENRE);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!genreName) return;

    try {
      // Creates the genre for the movies
      await createGenre({ variables: { genreName: genreName } });
      setGenreName("");
    } catch (err) {
      console.error("Error creating tag:", err);
    }
  };

  return (
    <div className="p-4 max-w-md mx-auto">
      <h1 className="text-xl font-bold mb-4">Create a New Genre</h1>
      <form
        onSubmit={handleSubmit}
        className="flex flex-col gap-2">
        <input
          type="text"
          value={genreName}
          onChange={(e) => setGenreName(e.target.value)}
          placeholder="Enter genre name"
          className="p-2 border rounded"
        />
        <button
          type="submit"
          disabled={loading}
          className="bg-blue-500 text-white px-4 py-2 rounded disabled:opacity-50">
          {loading ? "Creating..." : "Create Genre"}
        </button>
      </form>
      {error && <p className="text-red-500 mt-2">Error: {error.message}</p>}
      {data && <p className="text-green-500 mt-2">Created genre: {data.createGenre.genreName}</p>}
    </div>
  );
}
