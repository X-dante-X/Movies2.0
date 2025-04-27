// /components/layout/AppHeader.tsx
import React, { useState } from 'react';

interface AppHeaderProps {
  onSearch?: (query: string) => void;
}

const AppHeader: React.FC<AppHeaderProps> = ({ onSearch }) => {
  const [searchQuery, setSearchQuery] = useState('');

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchQuery(e.target.value);
    if (onSearch) {
      onSearch(e.target.value);
    }
  };

  return (
    <header className="bg-[#1e1e1e] w-full">
      <div className="container mx-auto px-4 py-2">
        <div className="flex items-center w-full">
          <div className="relative flex items-center w-full bg-gradient-to-r from-[rgba(195,207,226,0.25)] via-[rgba(222,226,248,0.25)] to-[rgba(226,226,242,0.25)] backdrop-blur-md rounded-lg shadow-md">
            <button className="flex items-center justify-center p-2 text-white">
              <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
              </svg>
            </button>
            <input
              type="text"
              className="w-full py-2 px-3 bg-transparent border-0 focus:outline-none text-white placeholder-white placeholder-opacity-75"
              placeholder="Filter by title"
              value={searchQuery}
              onChange={handleSearchChange}
            />
          </div>
        </div>
      </div>
    </header>
  );
};

export default AppHeader;