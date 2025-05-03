"use client";
import { useState } from 'react';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { useAuthStore } from "@/stores/useAuthStore";
import { CategoryType } from '../userpage/types';
import { getUserIdFromToken } from '@/utils/auth';
import StatusDropdown from './StatusDropdown';
import NotificationMessage from './NotificationMessage';
import ButtonContent from './ButtonContent';
import { axiosWithAuth } from '@/api/interceptors';

interface FavoriteButtonProps {
  movieId: number;
}

export default function FavoriteButton({ movieId }: FavoriteButtonProps) {
  const { user } = useAuthStore();
  const [isOpen, setIsOpen] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState(false);
  
  const userId = getUserIdFromToken();
  const queryClient = useQueryClient();

  const { mutate: updateFavorite, isPending: isLoading } = useMutation({
    mutationFn: async ({ 
      userId, 
      movieId, 
      status 
    }: { 
      userId: string; 
      movieId: number; 
      status: CategoryType 
    }) => {
      const response = await axiosWithAuth.post('http://localhost/favorites', {
        UserId: userId,
        MovieId: movieId,
        IsFavorite: true,
        Status: status
      });
      
      return response.data;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['favorites'] });
      setSuccess(true);
      setIsOpen(false);
      setTimeout(() => setSuccess(false), 3000);
    },
    onError: (err) => {
      setError(err instanceof Error ? err.message : 'An unknown error occurred');
    }
  });

  const toggleDropdown = () => {
    if (!user) {
      setError("Please log in to add this movie to your favorites");
      setTimeout(() => setError(null), 3000); 
      return;
    }
    
    setIsOpen(!isOpen);
    if (!isOpen) {
      setError(null);
      setSuccess(false);
    }
  };

  const handleSelectStatus = (statusValue: CategoryType) => {
    if (!user || !userId) return; 
    
    setError(null);
    updateFavorite({ userId, movieId, status: statusValue });
  };

  return (
    <div className="relative mt-4">
      {isOpen && user && (
        <StatusDropdown onSelectStatus={handleSelectStatus} />
      )}

      <button
        onClick={toggleDropdown}
        disabled={isLoading}
        className="bg-purple-600 hover:bg-purple-700 text-white font-bold rounded-full px-6 py-3 shadow-lg shadow-purple-900/40 flex items-center justify-center space-x-2 transition-all duration-300 transform hover:scale-105 disabled:opacity-70 disabled:cursor-not-allowed backdrop-blur-md border border-purple-400/30 relative overflow-hidden group"
      >
        <ButtonContent isLoading={isLoading} isOpen={isOpen} />
      </button>

      {error && (
        <NotificationMessage 
          type="error" 
          message={error} 
        />
      )}

      {success && (
        <NotificationMessage 
          type="success" 
          message="Added to favorites!" 
        />
      )}
    </div>
  );
}