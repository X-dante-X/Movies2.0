"use client"
import React from 'react';
import FilmCard from './FilmCard';
import { Film } from './types';

interface ListViewProps {
  films: Film[];
  error?: string | null;
}

const ListView: React.FC<ListViewProps> = ({ films, error = null }) => {
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

  return (
    <ul className="p-8 space-y-4">
      {films.map((film) => (
        <FilmCard key={film.id} film={film} />
      ))}
    </ul>
  );
};

export default ListView;