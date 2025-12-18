"use client"
import React from 'react';
import { useRouter } from 'next/navigation';
import { FilmCard } from './FilmCard';
import { Film } from './types';
import { useQueryClient } from '@tanstack/react-query';

interface ListViewProps {
  films: Film[];
  error?: string | null;
  onFilmsUpdate?: () => void;
}

export function ListView({ films, error = null, onFilmsUpdate }: ListViewProps) {
  const router = useRouter();
  const queryClient = useQueryClient();

  if (error) {
    return (
      <div className="p-8 text-center">
        <p className="text-red-500">Error: {error}</p>
      </div>
    );
  }

  if (films.length === 0) {
    return (
      <div className="p-8 text-center">
        <p className="text-gray-500">No favorites found.</p>
      </div>
    );
  }
  // redirects the user to the desired movie.
  const handleFilmClick = (filmId: number) => {
    router.push(`/movies/${filmId}`);
  };

  const handleDeleteSuccess = () => {
    if (onFilmsUpdate) {
      onFilmsUpdate();
    }
  };

  return (
    <ul className="p-8 space-y-4">
      {films.map((film) => (
        <li key={film.id} className="cursor-pointer">
          <FilmCard 
            film={film}
            queryClient={queryClient} 
            onClick={() => handleFilmClick(film.id)}
            onDeleteSuccess={handleDeleteSuccess}
          />
        </li>
      ))}
    </ul>
  );
}