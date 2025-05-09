// /components/layout/AppHeader.tsx
import { Search } from 'lucide-react';
import React, { useState } from 'react';


interface AppHeaderProps {
  onSearch?: (query: string) => void;
}

function AppHeader(props: AppHeaderProps) {
  const { onSearch } = props;
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
          <div className="relative">
              <Search className="h-5 w-5 text-gray-500" />
          </div>
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
}

export default AppHeader;