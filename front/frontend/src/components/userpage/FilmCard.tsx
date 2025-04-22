import React from 'react';
import { Card, CardMedia, CardContent, Box, Typography, IconButton } from '@mui/material';
import MoreVertIcon from '@mui/icons-material/MoreVert';

interface Film {
  title: string;
  description: string;
  posterPath: string;
  isFavorite: boolean;
  status: number; 
}

interface FilmCardProps {
  film: Film;
}

const FilmCard: React.FC<FilmCardProps> = ({ film }) => {
  const getStatusText = (status: number) => {
    switch (status) {
      case 1:
        return 'Watching';
      case 2:
        return 'Plan to Watch';
      case 3:
        return 'Completed';
      case 4:
        return 'Dropped';
      default:
        return 'Unknown';
    }
  };

  const getCurrentDate = () => {
    const date = new Date();
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();
    return `${day}.${month}.${year}`; // will then edit model on backend to list proper date idk
  };

  return (
    <Card 
      sx={{ 
        display: 'flex',
        mb: 2,
        background: 'linear-gradient(to bottom, #2e2e4f, #3a3c5a)',
        borderRadius: 2,
        color: '#e0e0f0',
        transition: 'transform 0.2s ease, box-shadow 0.3s ease',
        '&:hover': {
          transform: 'scale(1.02)',
          boxShadow: '0 6px 20px rgba(0,0,0,0.3)',
        },
      }}
    >
      <CardMedia
        component="img"
        sx={{ width: 100, height: 140, objectFit: 'cover' }}
        image={`/images/${film.posterPath}`} 
        alt={film.title}
      />
      <Box sx={{ display: 'flex', flexDirection: 'column', flexGrow: 1 }}>
        <CardContent sx={{ flex: '1 0 auto', pb: 1 }}>
          <Typography component="div" variant="h6" sx={{ color: 'white' }}>
            {film.title}
          </Typography>
          
          <Typography variant="body2" color="primary" sx={{ mt: 1 }}>
            {getStatusText(film.status)}
          </Typography>
          
          <Typography variant="body2" color="text.secondary" sx={{ color: '#aaa', mt: 1 }}>
            {film.description.length > 100 
              ? `${film.description.substring(0, 97)}...` 
              : film.description}
          </Typography>
        </CardContent>
      </Box>
      <Box sx={{ display: 'flex', alignItems: 'flex-start', p: 2 }}>
        <Typography variant="caption" color="text.secondary" sx={{ color: '#aaa' }}>
          Added<br />
          {getCurrentDate()} 
        </Typography>
        <IconButton size="small" sx={{ ml: 1, color: 'white' }}>
          <MoreVertIcon />
        </IconButton>
      </Box>
    </Card>
  );
};

export default FilmCard;