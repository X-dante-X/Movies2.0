"use client";
import { useState } from 'react';
import { useAuthStore } from "@/stores/useAuthStore";
import { CategoryType } from '../userpage/types'; 
import { jwtDecode } from 'jwt-decode';
import { getAccessToken } from '@/services/auth-token.service';

const STATUS_OPTIONS = [
  { value: 0 as CategoryType, label: "Plan to Watch", color: "bg-purple-500 hover:bg-purple-600", icon: "📅" },
  { value: 1 as CategoryType, label: "Watching", color: "bg-blue-500 hover:bg-blue-600", icon: "👁️" },
  { value: 2 as CategoryType, label: "Completed", color: "bg-green-500 hover:bg-green-600", icon: "✓" },
  { value: 3 as CategoryType, label: "Dropped", color: "bg-red-500 hover:bg-red-600", icon: "✗" },
  { value: 4 as CategoryType, label: "Favorite", color: "bg-yellow-500 hover:bg-yellow-600", icon: "★" }
];

interface FavoriteButtonProps {
  movieId: number;
}

export function FavoriteButton({ movieId }: FavoriteButtonProps) {
  const { user } = useAuthStore();
  const [isOpen, setIsOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState(false);
  
  const getUserId = () => {
    try {
      const token = getAccessToken();
      if (!token) {
        return null;
      }
      const decodedToken = jwtDecode(token) as { nameid: string };
      return decodedToken.nameid;
    } catch (err) {
      console.error("Error decoding token:", err);
      return null;
    }
  };
  
  const userId = getUserId();

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

  const handleSelectStatus = async (statusValue: CategoryType) => {
    if (!user || !userId) return; 
    
    setIsLoading(true);
    setError(null);
    
    const adjustedStatus = statusValue - 1; 
    const isFavorite = statusValue === 4; 
    
    try {
      const response = await fetch('http://localhost:5005/favorites', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          UserId: userId,
          MovieId: movieId,
          IsFavorite: isFavorite,
          Status: adjustedStatus  
        }),
      });

      if (!response.ok) {
        throw new Error('Failed to update favorite status');
      }

      setSuccess(true);
      setIsOpen(false);
      
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An unknown error occurred');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="relative">
      {isOpen && user && (
        <div className="absolute z-20 bottom-full mb-3 w-64 origin-bottom-right right-0 rounded-2xl shadow-2xl bg-gray-900/90 backdrop-blur-xl border border-purple-400/20 overflow-hidden transform transition-all duration-300 animate-fadeInUp">
          <div className="py-3 divide-y divide-gray-600/20">
            <div className="px-5 py-3 text-purple-300 text-sm font-medium uppercase tracking-wider">
              Add movie to:
            </div>
            {STATUS_OPTIONS.map((option) => (
              <button
                key={option.value}
                onClick={() => handleSelectStatus(option.value)}
                className={`w-full px-5 py-4 text-white hover:bg-purple-900/40 transition-all duration-200 flex items-center space-x-4 border-l-4 border-transparent hover:border-l-4 hover:border-${option.color.split(' ')[0].replace('bg-', '')}`}
              >
                <div className={`w-10 h-10 rounded-full ${option.color.split(' ')[0]} flex items-center justify-center text-lg shadow-lg shadow-black/30`}>
                  {option.icon}
                </div>
                <span className="font-medium text-left">{option.label}</span>
              </button>
            ))}
          </div>
          <div className="absolute bottom-0 right-8 transform translate-y-1/2 rotate-45 w-4 h-4 bg-gray-900/90 border-r border-b border-purple-400/20"></div>
        </div>
      )}

      <button
        onClick={toggleDropdown}
        disabled={isLoading}
        className="bg-purple-600 hover:bg-purple-700 text-white font-bold rounded-full px-6 py-3 shadow-lg shadow-purple-900/40 flex items-center justify-center space-x-2 transition-all duration-300 transform hover:scale-105 disabled:opacity-70 disabled:cursor-not-allowed backdrop-blur-md border border-purple-400/30 relative overflow-hidden group"
      >
        <div className="absolute inset-0 bg-gradient-to-r from-purple-400/20 to-pink-500/20 opacity-0 group-hover:opacity-100 transition-opacity duration-500"></div>
        
        <div className="absolute -inset-x-full top-0 bottom-0 w-1/2 transform -skew-x-12 bg-white opacity-10 animate-shimmer"></div>
        
        {isLoading ? (
          <svg className="animate-spin h-5 w-5 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
            <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
            <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
          </svg>
        ) : (
          <>
            <svg className="w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
              <path d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z" />
            </svg>
            <span className="relative z-10 tracking-wide">Add to Favorites</span>
            <svg className={`w-5 h-5 transition-transform duration-300 ${isOpen ? 'rotate-180 transform' : ''}`} fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M5 15l7-7 7 7"></path>
            </svg>
          </>
        )}
      </button>

      {error && (
        <div className="absolute mt-4 px-5 py-4 bg-red-500/90 backdrop-blur-xl rounded-xl text-white text-sm font-medium right-0 whitespace-nowrap z-30 shadow-lg shadow-red-900/50 border border-red-400/30 transform animate-fadeIn">
          <div className="flex items-center space-x-3">
            <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path>
            </svg>
            <span>{error}</span>
          </div>
        </div>
      )}

      {success && (
        <div className="absolute mt-4 px-5 py-4 bg-green-500/90 backdrop-blur-xl rounded-xl text-white text-sm font-medium right-0 z-30 shadow-lg shadow-green-900/50 border border-green-400/30 transform animate-fadeIn">
          <div className="flex items-center space-x-3">
            <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M5 13l4 4L19 7"></path>
            </svg>
            <span>Added to favorites!</span>
          </div>
        </div>
      )}
      
      <style jsx>{`
        @keyframes shimmer {
          0% { transform: translateX(-150%); }
          100% { transform: translateX(150%); }
        }
        .animate-shimmer {
          animation: shimmer 2.5s infinite;
        }
        @keyframes fadeIn {
          from { opacity: 0; transform: translateY(-10px); }
          to { opacity: 1; transform: translateY(0); }
        }
        .animate-fadeIn {
          animation: fadeIn 0.3s ease-out forwards;
        }
        @keyframes fadeInUp {
          from { opacity: 0; transform: translateY(10px); }
          to { opacity: 1; transform: translateY(0); }
        }
        .animate-fadeInUp {
          animation: fadeInUp 0.3s ease-out forwards;
        }
      `}</style>
    </div>
  );
}