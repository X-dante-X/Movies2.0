export interface Film {
  id: number;
  title: string;
  description: string;
  posterPath: string;
  isFavorite: boolean;
  status: number; 
}

export interface CategoryCounts {
  all: number;
  watching: number;
  plan: number;
  completed: number;
  dropped: number;
  favorite: number;
}

export type CategoryType = 'all' | 0 | 1 | 2 | 3;

export const CATEGORY_MAPPING = {
  'all': 'all',
  0: 'plan',       // Plan to watch
  1: 'watching',   // Watching
  2: 'completed',  // Completed
  3: 'dropped',    // Dropped
} as const;

export type ViewModeType = 'list';
export type SortOptionId = 'name_asc' | 'name_desc' | 'date_added' | 'chapter_updated';

export interface SortOption {
  id: SortOptionId;
  label: string;
}