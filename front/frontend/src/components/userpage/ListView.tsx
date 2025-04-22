"use client"
import React from 'react';
import { List, Box, Typography } from '@mui/material';
import FilmCard from './FilmCard';
import { Film } from './types';

interface ListViewProps {
  films: Film[];
  error?: string | null;
}

const ListView: React.FC<ListViewProps> = ({ films, error = null }) => {
  if (error) {
    return (
      <Box sx={{ p: 2, textAlign: 'center' }}>
        <Typography color="error">Error: {error}</Typography>
      </Box>
    );
  }

  if (films.length === 0) {
    return (
      <Box sx={{ p: 2, textAlign: 'center' }}>
        <Typography>No favorites found.</Typography>
      </Box>
    );
  }

  return (
    <List sx={{ p: 2 }}>
      {films.map((film) => (
        <FilmCard key={film.id} film={film} />
      ))}
    </List>
  );
};

export default ListView;