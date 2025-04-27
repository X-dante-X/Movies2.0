"use client"
import React, { useState, useEffect, useMemo } from 'react';
import { Box } from '@mui/material';
import CircularProgress from '@mui/material/CircularProgress';
import Sidebar from '../../components/userpage/Sidebar';
import AppHeader from '../../components/userpage/AppHeader';
import ListView from '../../components/userpage/ListView';
import { getFilteredFilms, getCategoryCounts } from '../../utils/filterMovies';
import { CategoryType, ViewModeType, Film } from '../../components/userpage/types';
import { jwtDecode} from 'jwt-decode';
import { getAccessToken } from '@/services/auth-token.service';

const UserFilmPage: React.FC = () => {
  const [selectedCategory, setSelectedCategory] = useState<CategoryType>(0);
  const [viewMode, setViewMode] = useState<ViewModeType>('list');
  const [searchQuery, setSearchQuery] = useState<string>('');
  const [films, setFilms] = useState<Film[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  
  useEffect(() => {
    const fetchFilms = async () => {
      try {
        setLoading(true);
        
        const token = getAccessToken()
        
        if (!token) {
          throw new Error('No authentication token found');
        }
        const decodedToken = jwtDecode(token) as { nameid: string };
        const userId = decodedToken.nameid;
        // should change it 
        const response = await fetch(`http://localhost:5005/favorites/${userId}`, {
          headers: {
            'Authorization': `Bearer ${token}`
          }
        });
        
        if (!response.ok) {
          throw new Error('Failed to fetch favorites');
        }
        
        const data = await response.json();
        setFilms(data);
      } catch (err) {
        console.error('Error fetching films:', err);
        setError(err instanceof Error ? err.message : 'An unknown error occurred');
      } finally {
        setLoading(false);
      }
    };
    
    fetchFilms();
  }, []);

  const categoryCounts = useMemo(() => getCategoryCounts(films), [films]);

  const filteredFilms = useMemo(() => {
    let result = getFilteredFilms(films, selectedCategory);
    
    if (searchQuery) {
      result = result.filter(film => 
        film.title.toLowerCase().includes(searchQuery.toLowerCase())
      );
    }
    
    return result;
  }, [films, selectedCategory, searchQuery]);

  return (
    <Box 
      sx={{
        display: 'flex',
        minHeight: '100vh',
        color: 'white',
        bgcolor: 'transparent',
        backgroundImage: 'linear-gradient(to right, #c3cfe2, #dee2f8, #e2e2f2)',
        backgroundAttachment: 'fixed'
      }}
    >
      <Sidebar 
        selectedCategory={selectedCategory}
        onCategoryChange={setSelectedCategory}
        categoryCounts={categoryCounts}
        viewMode={viewMode}
        onViewModeChange={setViewMode}
      />

      <Box component="main" sx={{ flexGrow: 1 }}>
        <AppHeader onSearch={setSearchQuery} />
        
        {viewMode === 'list' && 
          (loading ? (
            <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '300px' }}>
              <CircularProgress />
            </Box>
          ) : (
            <ListView films={filteredFilms} error={error} />
          ))
        }
      </Box>
    </Box>
  );
};

export default UserFilmPage;