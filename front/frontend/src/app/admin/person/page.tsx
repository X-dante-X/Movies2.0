"use client";

import { useState } from "react";
import { gql, useMutation, useQuery } from "@apollo/client";
import { GET_COUNTRIES } from "@/graphql/queries";
import { GetCountryDetailsResponse } from "@/types/movie.types";

const CREATE_PERSON = gql`
  mutation CreatePerson($personName: String!, $gender: String!, $dateOfBirth: DateTime!, $countryId: Int!, $biography: String!) {
    createPerson(personDTO: { personName: $personName, gender: $gender, dateOfBirth: $dateOfBirth, countryId: $countryId, biography: $biography }) {
      personName
    }
  }
`;

export default function Page() {
  const [personName, setPersonName] = useState("");
  const [gender, setGender] = useState("");
  const [dateOfBirth, setDateOfBirth] = useState("");
  const [countryId, setCountryId] = useState<number | null>(null);
  const [biography, setBiography] = useState("");

  const { data: countriesData, loading: countriesLoading } = useQuery<GetCountryDetailsResponse>(GET_COUNTRIES);
  const [createPerson, { data, loading, error }] = useMutation(CREATE_PERSON);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!personName || !gender || !dateOfBirth || !countryId || !biography) return;

    try {
      const formattedDateOfBirth = new Date(dateOfBirth + "T00:00:00Z").toISOString();
      await createPerson({ variables: { personName, gender, dateOfBirth: formattedDateOfBirth, countryId, biography } });
      setPersonName("");
      setGender("");
      setDateOfBirth("");
      setCountryId(null);
      setBiography("");
    } catch (err) {
      console.error("Error creating person:", err);
    }
  };

  return (
    <div className="p-4 max-w-md mx-auto">
      <h1 className="text-xl font-bold mb-4">Create a New Person</h1>
      <form
        onSubmit={handleSubmit}
        className="flex flex-col gap-2">
        <input
          type="text"
          value={personName}
          onChange={(e) => setPersonName(e.target.value)}
          placeholder="Enter person name"
          className="p-2 border rounded"
        />
        <select
          value={gender}
          onChange={(e) => setGender(e.target.value)}
          className="p-2 border rounded">
          <option
            value=""
            disabled>
            Select gender
          </option>
          <option value="Male">Male</option>
          <option value="Female">Female</option>
          <option value="Other">Other</option>
        </select>
        <input
          type="date"
          value={dateOfBirth}
          onChange={(e) => setDateOfBirth(e.target.value)}
          className="p-2 border rounded"
        />
        <select
          value={countryId || ""}
          onChange={(e) => setCountryId(Number(e.target.value))}
          className="p-2 border rounded"
          disabled={countriesLoading}>
          <option
            value=""
            disabled>
            Select nationality
          </option>
          {countriesData?.countries.map((country) => (
            <option
              key={country.countryId}
              value={country.countryId}>
              {country.countryName}
            </option>
          ))}
        </select>
        <input
          type="text"
          value={biography}
          onChange={(e) => setBiography(e.target.value)}
          placeholder="Enter biography"
          className="p-2 border rounded"
        />
        <button
          type="submit"
          disabled={loading}
          className="bg-blue-500 text-white px-4 py-2 rounded disabled:opacity-50">
          {loading ? "Creating..." : "Create Person"}
        </button>
      </form>
      {error && <p className="text-red-500 mt-2">Error: {error.message}</p>}
      {data && <p className="text-green-500 mt-2">Created person: {data.createPerson.personName}</p>}
    </div>
  );
}
