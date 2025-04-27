"use client";
import { useQuery } from "@apollo/client";
import { GET_MOVIES, GET_FILTERS, Movie, MovieOrderField, SortDirection } from "@/graphql/movie.query";
import { Card } from "@/components/media/Card";
import { AnimatePresence, m } from "framer-motion";
import { useEffect, useState, useRef } from "react";
import MovieFilters, { SelectedFilters } from "./MovieFilters";

const PAGE_SIZE = 20;

export default function Page() {
  const [filters, setFilters] = useState<SelectedFilters>({
    genreIds: [],
    countryIds: [],
    companyIds: [],
    tagIds: [],
    strictGenres: false,
    strictTags: false,
  });
  const [sort, setSort] = useState<{
    field: MovieOrderField;
    direction: SortDirection;
  }>({
    field: MovieOrderField.POPULARITY,
    direction: SortDirection.DESC,
  });
  const [showFilters, setShowFilters] = useState(false);
  const [movies, setMovies] = useState<Movie[]>([]);
  const [afterCursor, setAfterCursor] = useState<string | null>(null);
  const [hasNextPage, setHasNextPage] = useState(true);

  const { data: fData, loading: fLoading, error: fError } = useQuery(GET_FILTERS);

  const { data, loading, error, fetchMore, refetch } = useQuery(GET_MOVIES, {
    variables: {
      first: PAGE_SIZE,
      after: null,
      where: buildWhere(filters),
      order: [buildSortObject(sort)],
    },
    fetchPolicy: "cache-and-network",
  });

  const observerRef = useRef<HTMLDivElement>(null);

  function buildSortObject(s: { field: MovieOrderField; direction: SortDirection }) {
    return { [s.field]: s.direction };
  }

  interface WhereInput {
    genre?: { some: { genreId: { in: SelectedFilters["genreIds"] } } };
    tags?: { some: { tagId: { in: SelectedFilters["tagIds"] } } };
    productionCountry?: { countryId: { in: SelectedFilters["countryIds"] } };
    productionCompany?: { companyId: { in: SelectedFilters["companyIds"] } };
  }

  function buildWhere(f: SelectedFilters): WhereInput {
    const where: WhereInput = {};

    if (f.genreIds.length) {
      where.genre = f.strictGenres ? { some: { genreId: { in: f.genreIds } } } : { some: { genreId: { in: f.genreIds } } };
    }

    if (f.tagIds.length) {
      where.tags = f.strictTags ? { some: { tagId: { in: f.tagIds } } } : { some: { tagId: { in: f.tagIds } } };
    }

    if (f.countryIds.length) {
      where.productionCountry = { countryId: { in: f.countryIds } };
    }
    if (f.companyIds.length) {
      where.productionCompany = { companyId: { in: f.companyIds } };
    }

    return where;
  }

  useEffect(() => {
    if (data?.movies?.nodes) {
      setMovies(data.movies.nodes);
      setAfterCursor(data.movies.pageInfo.endCursor);
      setHasNextPage(data.movies.pageInfo.hasNextPage);
    }
  }, [data]);

  useEffect(() => {
    refetch({
      first: PAGE_SIZE,
      after: null,
      where: buildWhere(filters),
      order: [buildSortObject(sort)],
    });
  }, [filters, sort, refetch]);

  useEffect(() => {
    const obs = new IntersectionObserver(
      async ([entry]) => {
        if (entry.isIntersecting && hasNextPage && afterCursor) {
          const { data: more } = await fetchMore({
            variables: {
              after: afterCursor,
              first: PAGE_SIZE,
              where: buildWhere(filters),
              order: [buildSortObject(sort)],
            },
          });
          setMovies((prev) => [...prev, ...more.movies.nodes]);
          setAfterCursor(more.movies.pageInfo.endCursor);
          setHasNextPage(more.movies.pageInfo.hasNextPage);
        }
      },
      { threshold: 1.0 }
    );
    if (observerRef.current) obs.observe(observerRef.current);
    return () => obs.disconnect();
  }, [afterCursor, hasNextPage, fetchMore, filters, sort]);

  if (loading && !movies.length) return <p className="text-xl text-center">Loading...</p>;
  if (error) return <p className="text-xl text-center text-red-500">Error: {error.message}</p>;

  return (
    <>
      <div className="fixed inset-0 bg-gradient-to-br from-gray-900/20 via-indigo-900/20 to-purple-900/20 z-[-1]" />
      <div className="relative p-4 mx-16 min-h-screen">
        <div className="mb-4 flex flex-wrap gap-4 items-center">
          <button
            onClick={() => setShowFilters((prev) => !prev)}
            className="bg-indigo-600 hover:bg-indigo-700 text-white px-4 py-2 rounded">
            Filters
          </button>
          <AnimatePresence>
            {showFilters && !fLoading && !fError && fData && (
              <MovieFilters
                filtersData={{
                  genres: fData.genres,
                  countries: fData.countries,
                  productionCompanies: fData.productionCompanies,
                  tags: fData.tags,
                }}
                selectedFilters={filters}
                onChange={setFilters}
                onClose={() => setShowFilters(false)}
              />
            )}
          </AnimatePresence>

          <select
            value={`${sort.field}-${sort.direction}`}
            onChange={(e) => {
              const [field, direction] = e.target.value.split("-");
              setSort({
                field: field as MovieOrderField,
                direction: direction as SortDirection,
              });
            }}
            className="bg-indigo-600 hover:bg-indigo-700 text-white px-4 py-2.5 rounded">
            {Object.values(MovieOrderField).map((field) =>
              Object.values(SortDirection).map((direction) => (
                <option
                  key={`${field}-${direction}`}
                  value={`${field}-${direction}`}>
                  {`${field.replace(/([A-Z])/g, " $1").trim()} ${direction === SortDirection.ASC ? "↑" : "↓"}`}
                </option>
              ))
            )}
          </select>
        </div>

        <div className="grid grid-cols-3 sm:grid-cols-4 lg:grid-cols-5 xl:grid-cols-6 gap-x-1 gap-y-6">
          {movies.map((movie) => (
            <m.div
              key={movie.movieId}
              animate={{ scale: 1 }}
              className="relative cursor-pointer">
              <Card
                movieId={movie.movieId}
                title={movie.title}
                popularity={movie.popularity}
                ReleaseDate={movie.releaseDate}
                posterFile={movie.posterPath}
              />
            </m.div>
          ))}
        </div>

        <div
          ref={observerRef}
          className="h-10"
        />
      </div>
    </>
  );
}
