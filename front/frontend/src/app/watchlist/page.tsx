"use client"
import React, { useState, useMemo } from 'react';
import { useQuery } from '@tanstack/react-query';
import { Loader } from 'lucide-react';
import { Sidebar } from '../../components/userpage/Sidebar';
import AppHeader from '../../components/userpage/AppHeader';
import { ListView } from '../../components/userpage/ListView';
import { getFilteredFilms, getCategoryCounts } from '../../utils/filterMovies';
import { CategoryType, ViewModeType, Film } from '../../components/userpage/types';
import { getAccessToken } from '@/services/auth-token.service';
import { axiosWithAuth } from '@/api/interceptors';

const fetchFavorites = async (): Promise<Film[]> => {
  const token = getAccessToken();
  
  if (!token) {
    throw new Error('No authentication token found');
  }
  
  const response = await axiosWithAuth.get(`http://localhost/favorites`);
  return response.data;
};

export default function Page() {
  const [selectedCategory, setSelectedCategory] = useState<CategoryType>('all');
  const [viewMode, setViewMode] = useState<ViewModeType>('list');
  const [searchQuery, setSearchQuery] = useState<string>('');
  
  const { 
    data: films = [] as Film[], 
    isLoading, 
    error 
  } = useQuery<Film[]>({
    queryKey: ['favorites'],
    queryFn: fetchFavorites,
    staleTime: 5 * 60 * 1000, //  5 mins
  });

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
    <div className="flex min-h-screen text-white bg-transparent bg-gradient-to-r from-[#c3cfe2] to-[#e2e2f2] bg-fixed">
      <Sidebar 
        selectedCategory={selectedCategory}
        onCategoryChange={setSelectedCategory}
        categoryCounts={categoryCounts}
        viewMode={viewMode}
        onViewModeChange={setViewMode}
      />

      <main className="flex-grow">
        <AppHeader onSearch={setSearchQuery} />
        
        {viewMode === 'list' && 
          (isLoading ? (
            <div className="flex justify-center items-center h-[300px]">
              <Loader className="animate-spin text-gray-700" size={36} />
            </div>
          ) : (
            <ListView 
              films={filteredFilms} 
              error={error instanceof Error ? error.message : error ? String(error) : null} 
            />
          ))
        }
      </main>
    </div>
  );
}