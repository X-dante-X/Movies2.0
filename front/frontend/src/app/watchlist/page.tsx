"use client"
import React, { useState, useEffect, useMemo } from 'react';
import { Loader } from 'lucide-react';
import { Sidebar } from '../../components/userpage/Sidebar';
import AppHeader from '../../components/userpage/AppHeader';
import { ListView } from '../../components/userpage/ListView';
import { getFilteredFilms, getCategoryCounts } from '../../utils/filterMovies';
import { CategoryType, ViewModeType, Film } from '../../components/userpage/types';
import { jwtDecode } from 'jwt-decode';
import { getAccessToken } from '@/services/auth-token.service';
import { axiosWithAuth } from '@/api/interceptors';

export default function Page() {
  const [selectedCategory, setSelectedCategory] = useState<CategoryType>('all');
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
        const response = await axiosWithAuth.get(`http://localhost/favorites/${userId}`);

        const data = response.data;
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
          (loading ? (
            <div className="flex justify-center items-center h-[300px]">
              <Loader className="animate-spin text-gray-700" size={36} />
            </div>
          ) : (
            <ListView films={filteredFilms} error={error} />
          ))
        }
      </main>
    </div>
  );
}