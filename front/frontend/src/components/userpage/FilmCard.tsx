"use client"
import React, { useState, useRef, useEffect } from 'react';
import Image from 'next/image';
import { MoreVertical, Trash } from 'lucide-react';
import { getServerMinIoUrl } from '@/utils/getMinIoUrl';
import { Film } from './types';
import { useMutation } from '@tanstack/react-query';
import { QueryClient } from '@tanstack/react-query';
import { axiosWithAuth } from "@/api/interceptors";
import { getUserIdFromToken } from '@/utils/auth';

interface FilmCardProps {
  film: Film;
  queryClient: QueryClient;
  onClick?: () => void;
  onDeleteSuccess?: () => void;
}

export function FilmCard({ film, queryClient, onClick, onDeleteSuccess }: FilmCardProps) {
  const userId = getUserIdFromToken();
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const [success, setSuccess] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const menuRef = useRef<HTMLDivElement>(null);

  const { mutate: deleteFilm, isPending } = useMutation({
    mutationFn: async () => {
      const response = await axiosWithAuth.post("http://localhost/favorites/delete", {
        UserId: userId,
        MovieId: film.id
      });
      return response.data;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["userFilms"] });
      setSuccess(true);
      setIsMenuOpen(false);
      if (onDeleteSuccess) onDeleteSuccess();
      setTimeout(() => setSuccess(false), 3000);
    },
    onError: (err) => {
      setError(err instanceof Error ? err.message : "An error occurred");
      setTimeout(() => setError(null), 3000);
    },
  });

  useEffect(() => {
    function handleClickOutside(event: MouseEvent) {
      if (menuRef.current && !menuRef.current.contains(event.target as Node)) {
        setIsMenuOpen(false);
      }
    }

    document.addEventListener('mousedown', handleClickOutside);
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, []);

  const getStatusText = (status: number) => {
    switch (status) {
      case 0:
        return 'Plan to Watch';
      case 1:
        return 'Watching';
      case 2:
        return 'Completed';
      case 3:
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
    return `${day}.${month}.${year}`;
  };

  const handleDelete = (e: React.MouseEvent) => {
    e.stopPropagation(); 
    deleteFilm();
  };

  const handleCardClick = () => {
    if (!isMenuOpen && onClick) {
      onClick();
    }
  };

  const handleMenuToggle = (e: React.MouseEvent) => {
    e.stopPropagation();
    setIsMenuOpen(!isMenuOpen);
  };

  return (
    <>
      {success && (
        <div className="fixed top-4 right-4 bg-green-500 text-white px-4 py-2 rounded-md shadow-lg z-50">
          Film successfully removed
        </div>
      )}
      
      {error && (
        <div className="fixed top-4 right-4 bg-red-500 text-white px-4 py-2 rounded-md shadow-lg z-50">
          {error}
        </div>
      )}
      
      <div 
        className="flex mb-2 rounded-lg overflow-hidden bg-gradient-to-b from-[#2e2e4f] to-[#3a3c5a] text-[#e0e0f0] transition-all duration-200 ease-in-out hover:transform hover:scale-102 hover:shadow-lg"
        onClick={handleCardClick}
      >
        <div className="w-[100px] h-[140px] flex-shrink-0">
          <Image
            src={getServerMinIoUrl(film.posterPath)}
            alt={film.title}
            width={100}
            height={140}
            className="w-full h-full object-cover"
          />
        </div>

        <div className="flex flex-col flex-grow">
          <div className="p-4 pb-1 flex-grow">
            <h6 className="text-white text-lg font-medium">
              {film.title}
            </h6>

            <p className="text-blue-400 mt-1 text-sm">
              {getStatusText(film.status)}
            </p>

            <p className="text-[#aaa] mt-1 text-sm">
              {film.description.length > 100 
                ? `${film.description.substring(0, 97)}...` 
                : film.description}
            </p>
          </div>
        </div>
        <div className="flex items-start p-2">
          <div className="text-[#aaa] text-xs">
            Added<br />
            {getCurrentDate()}
          </div>
          <div className="relative" ref={menuRef}>
            <button 
              className="ml-1 text-white p-1 hover:bg-gray-700 rounded-full"
              onClick={handleMenuToggle}
              disabled={isPending}
            >
              <MoreVertical size={18} />
            </button>
            
            {isMenuOpen && (
              <div className="absolute right-0 top-8 w-40 bg-[#2a2a40] rounded-md shadow-lg z-10 border border-gray-700">
                <button 
                  className="flex items-center w-full px-3 py-2 text-sm text-red-400 hover:bg-gray-700"
                  onClick={handleDelete}
                  disabled={isPending}
                >
                  {isPending ? (
                    <span className="flex items-center">
                      <svg className="animate-spin -ml-1 mr-2 h-4 w-4 text-red-400" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                        <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
                        <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                      </svg>
                      Deleting...
                    </span>
                  ) : (
                    <>
                      <Trash size={16} className="mr-2" />
                      Delete
                    </>
                  )}
                </button>
              </div>
            )}
          </div>
        </div>
      </div>
    </>
  );
}