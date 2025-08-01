import React from 'react';
import { CATEGORIES, CATEGORY_ICONS, SORT_OPTIONS } from './constants';
import { CategoryCounts, CategoryType, ViewModeType } from './types';
import { Menu } from 'lucide-react';

const getCategoryKey = (category: CategoryType): keyof CategoryCounts => {
  switch(category) {
    case 'all': return 'all';
    case 0: return 'plan';
    case 1: return 'watching';
    case 2: return 'completed';
    case 3: return 'dropped';
    default: return 'all';
  }
};

interface SidebarProps {
  selectedCategory: CategoryType;
  onCategoryChange: (category: CategoryType) => void;
  categoryCounts: CategoryCounts;
  viewMode: ViewModeType;
  onViewModeChange: (mode: ViewModeType) => void;
}

export function Sidebar({ 
  selectedCategory, 
  onCategoryChange, 
  categoryCounts, 
  viewMode, 
  onViewModeChange 
}: SidebarProps) {
  return (
    <div className="hidden sm:block w-60 bg-[#2e2e4f] text-[#e0e0f0] border-r border-[#444] shadow-lg">
      <div className="p-4">
        <h2 className="text-xl font-medium">Lists</h2>
      </div>

      <div className="h-px bg-[#333]"></div>

      <ul>
        {Object.entries(CATEGORIES).map(([key, value]) => {
          const categoryKey = getCategoryKey(value);
          const IconComponent = CATEGORY_ICONS[value];

          return (
            <li key={String(value)}>
              <button
                className={`flex items-center w-full px-4 py-2 hover:bg-[#333] ${selectedCategory === value ? 'bg-[#333]' : ''}`}
                onClick={() => onCategoryChange(value)}
              >
                <span className="text-white mr-4">
                  <IconComponent />
                </span>
                <span className="flex-grow">{key.charAt(0) + key.slice(1).toLowerCase()}</span>
                <span className="inline-flex items-center justify-center min-w-6 h-6 px-2 rounded-full text-xs bg-blue-600 text-white">
                  {categoryCounts[categoryKey]}
                </span>
              </button>
            </li>
          );
        })}
      </ul>

      <div className="h-px bg-[#333] mt-4"></div>

      <div className="p-4">
        <h3 className="text-sm font-medium text-[#aaa]">View</h3>
        <ul>
          <li>
            <button
              className={`flex items-center w-full px-4 py-2 hover:bg-[#333] ${viewMode === 'list' ? 'bg-[#333]' : ''}`}
              onClick={() => onViewModeChange('list')}
            >
              <span className="text-white mr-4">
              <div className="relative">
                <Menu className="h-5 w-5 text-current" />
              </div>
              </span>
              <span>List</span>
            </button>
          </li>
        </ul>
      </div>

      <div className="h-px bg-[#333]"></div>

      <div className="p-4">
        <h3 className="text-sm font-medium text-[#aaa]">Sort By</h3>
        <ul>
          {SORT_OPTIONS.map(option => (
            <li key={option.id}>
              <button className="flex items-center w-full px-4 py-2 hover:bg-[#333]">
                <span>{option.label}</span>
              </button>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
}