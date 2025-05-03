"use client"
import React from 'react';
import { useRouter } from 'next/navigation';
import { FilmCard } from './FilmCard';
import { Film } from './types';

interface ListViewProps {
  films: Film[];
  error?: string | null;
}

export function ListView({ films, error = null }: ListViewProps) {
  const router = useRouter();

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

  const handleFilmClick = (filmId: number) => {
    router.push(`/movies/${filmId}`);
  };

  return (
    <ul className="p-8 space-y-4">
      {films.map((film) => (
        <li 
          key={film.id} 
          onClick={() => handleFilmClick(film.id)}
          className="cursor-pointer"
        >
          <FilmCard film={film} />
        </li>
      ))}
    </ul>
  );
}