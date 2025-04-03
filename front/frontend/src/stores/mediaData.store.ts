"use client";

import { create } from "zustand";
import { gql } from "@apollo/client";
import { TFilter } from "@/components/filters/filters.data";
import { getClient } from "@/lib/apolloClient";
import { IMediaItem } from "@/types/movie.types";
import { useFilterStore } from "./filter.store";

interface IMediaStore {
    mediaItems: IMediaItem[];
    setMediaItems: (items: IMediaItem[]) => void;
    fetchMovies: (filter: TFilter) => Promise<void>;
}

const GET_MOVIES = gql`
  query GetMovies($orderBy: [MovieSortInput!]) {
    movies(first: 12, order: $orderBy) {
      nodes {
        movieId
        title
        releaseDate
        popularity
        posterPath
        backdropPath
        genre {
          genreId
          genreName
        }
      }
    }
  }
`;

const filterToOrderBy: Record<TFilter, { [key: string]: string }[]> = {
    Popular: [{ popularity: "DESC" }],
    Latest: [{ releaseDate: "DESC" }],
    "Top Rated": [{ voteAverage: "DESC" }],
    Recommended: [{ voteCount: "DESC" }],
};

export const useMediaStore = create<IMediaStore>((set) => ({
    mediaItems: [],
    setMediaItems: (items) => set({ mediaItems: items }),
    fetchMovies: async (filter) => {
        const client = getClient();
        const orderBy = filterToOrderBy[filter] || [];

        const { data } = await client.query<{ movies: { nodes: IMediaItem[] } }>({
            query: GET_MOVIES,
            variables: { orderBy },
        });

        if (data?.movies?.nodes) {
            const mediaItemsWithIds = data.movies.nodes.map((item, index) => ({
                ...item,
                id: index + 1,
            }));

            set({ mediaItems: mediaItemsWithIds });
        }
    },
}));

useFilterStore.subscribe((state) => {
    useMediaStore.getState().fetchMovies(state.currentFilter);
});

useMediaStore.getState().fetchMovies(useFilterStore.getState().currentFilter);
