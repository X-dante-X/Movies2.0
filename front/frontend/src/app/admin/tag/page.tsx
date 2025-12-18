"use client";

import { useState } from "react";
import { gql, useMutation } from "@apollo/client";

const CREATE_TAG = gql`
  mutation CreateTag($tagName: String!) {
    createTag(tag: { tagId: 0, tagName: $tagName }) {
      tagId
      tagName
    }
  }
`;

export default function Page() {
  const [tagName, setTagName] = useState("");
  const [createTag, { data, loading, error }] = useMutation(CREATE_TAG);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!tagName) return;

    try {
      // Creates tag with the specified name
      await createTag({ variables: { tagName } });
      setTagName("");
    } catch (err) {
      console.error("Error creating tag:", err);
    }
  };

  return (
    <div className="p-4 max-w-md mx-auto">
      <h1 className="text-xl font-bold mb-4">Create a New Tag</h1>
      <form
        onSubmit={handleSubmit}
        className="flex flex-col gap-2">
        <input
          type="text"
          value={tagName}
          onChange={(e) => setTagName(e.target.value)}
          placeholder="Enter tag name"
          className="p-2 border rounded"
        />
        <button
          type="submit"
          disabled={loading}
          className="bg-blue-500 text-white px-4 py-2 rounded disabled:opacity-50">
          {loading ? "Creating..." : "Create Tag"}
        </button>
      </form>
      {error && <p className="text-red-500 mt-2">Error: {error.message}</p>}
      {data && <p className="text-green-500 mt-2">Created tag: {data.createTag.tagName}</p>}
    </div>
  );
}
