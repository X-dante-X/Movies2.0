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
  
  export type CategoryType = 0 | 1 | 2 | 3| 4 | 5;

  export const CATEGORY_MAPPING = {
    0: 'all',
    1: 'watching',
    2: 'plan',
    3: 'completed',
    4: 'dropped',
    5: 'favorite'
  } as const;

  
  export type ViewModeType = 'list';
  export type SortOptionId = 'name_asc' | 'name_desc' | 'date_added' | 'chapter_updated';
  
  export interface SortOption {
    id: SortOptionId;
    label: string;
  }
  