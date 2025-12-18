"use client";

import { useState } from "react";
import { gql, useMutation, useQuery } from "@apollo/client";
import { GET_COUNTRIES } from "@/graphql/queries";
import { GetCountryDetailsResponse } from "@/types/movie.types";

const CREATE_PRODUCTION_COMPANY = gql`
  mutation CreateProductionCompany($companyName: String!, $logo: Upload!, $countryId: Int!) {
    createProductionCompany(productionCompanyDTO: { companyName: $companyName, logo: $logo, countryId: $countryId }) {
      companyName
    }
  }
`;

export default function Page() {
  const [companyName, setCompanyName] = useState("");
  const [countryId, setCountryId] = useState<number | null>(null);
  const [logo, setLogo] = useState<File | null>(null);
  const { data: countriesData, loading: countriesLoading } = useQuery<GetCountryDetailsResponse>(GET_COUNTRIES);
  const [createProductionCompany, { data, loading, error }] = useMutation(CREATE_PRODUCTION_COMPANY);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!companyName || !logo || !countryId) return;

    try {
      // creates the production company
      await createProductionCompany({ variables: { companyName, logo, countryId } });
      setCompanyName("");
      setCountryId(null);
      setLogo(null);
    } catch (err) {
      console.error("Error creating production company:", err);
    }
  };

  return (
    <div className="p-4 max-w-md mx-auto">
      <h1 className="text-xl font-bold mb-4">Create a New Production Company</h1>
      <form
        onSubmit={handleSubmit}
        className="flex flex-col gap-2">
        <input
          type="text"
          value={companyName}
          onChange={(e) => setCompanyName(e.target.value)}
          placeholder="Enter company name"
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
            Select a country
          </option>
          {countriesData?.countries.map((country) => (
            <option
              key={country.countryId}
              value={country.countryId}>
              {country.countryName}
            </option>
          ))}
        </select>
        <label className="p-2 border rounded cursor-pointer">
          Choose logo
          <input
            type="file"
            accept="image/*"
            onChange={(e) => {
              const file = e.target.files?.[0] || null;
              setLogo(file);
            }}
            className="hidden"
          />
        </label>

        <button
          type="submit"
          disabled={loading}
          className="bg-blue-500 text-white px-4 py-2 rounded disabled:opacity-50">
          {loading ? "Creating..." : "Create Production Company"}
        </button>
      </form>
      {error && <p className="text-red-500 mt-2">Error: {error.message}</p>}
      {data && <p className="text-green-500 mt-2">Created company: {data.createProductionCompany.companyName}</p>}
    </div>
  );
}
