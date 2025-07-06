import { FIND_MOVIES_BY_TITLE, FindMoviesData } from "@/graphql/movie.query";
import { useQuery } from "@apollo/client";
import { m, AnimatePresence } from "framer-motion";
import { Search } from "lucide-react";
import Link from "next/link";
import { useState, useRef, useEffect } from "react";
import { useDebounce } from "use-debounce";

export function SearchButton() {
  const [open, setOpen] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");
  const [debouncedSearchTerm] = useDebounce(searchTerm, 500);
  const containerRef = useRef<HTMLDivElement>(null);

  const { data, loading } = useQuery<FindMoviesData>(FIND_MOVIES_BY_TITLE, {
    variables: {
      partOfTitle: debouncedSearchTerm,
    },
    skip: !debouncedSearchTerm,
  });

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (containerRef.current && !containerRef.current.contains(event.target as Node)) {
        setOpen(false);
        setSearchTerm("");
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  return (
    <div
      ref={containerRef}
      className="relative flex items-center">
      {!open ? (
        <button onClick={() => setOpen(true)}>
          <Search className="hover:text-primary transition-colors self-center align-middle" />
        </button>
      ) : (
        <m.div
          initial={{ width: 40 }}
          animate={{ width: 300 }}
          exit={{ width: 40 }}
          transition={{ duration: 0.3 }}
          className="flex items-center rounded-xl bg-white">
          <Search className="text-muted-foreground mr-2" />
          <input
            autoFocus
            type="text"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            placeholder="Search movies..."
            className="w-full outline-none bg-transparent text-sm text-black"
          />
        </m.div>
      )}

      <AnimatePresence>
        {open && debouncedSearchTerm && (
          <m.div
            initial={{ opacity: 0, y: -5 }}
            animate={{ opacity: 1, y: 0 }}
            exit={{ opacity: 0, y: -5 }}
            transition={{ duration: 0.2 }}
            className="absolute z-50 mt-2 top-6 w-80 bg-white border rounded-lg shadow-lg p-2">
            {loading ? (
              <p className="text-sm text-gray-500">Loading...</p>
            ) : data?.findMoviesByTitle?.nodes?.length ? (
              <ul>
                {data.findMoviesByTitle.nodes.map((movie) => (
                  <li
                    key={movie.movieId}
                    className="mb-2">
                    <Link
                      href={`/movies/${movie.movieId}`}
                      className="block">
                      <div className="font-medium text-blue-600">{movie.title}</div>
                      <p className="text-xs text-gray-500">
                        {new Date(movie.releaseDate).getFullYear()} &bull; Rating: {movie.popularity.toFixed(1)}
                      </p>
                    </Link>
                  </li>
                ))}
              </ul>
            ) : (
              <p className="text-sm text-gray-500">No results found</p>
            )}
          </m.div>
        )}
      </AnimatePresence>
    </div>
  );
}
