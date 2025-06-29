import { 
  Bookmark,
  Film,
  Clock,
  XCircle,
  CheckCircle,
  LucideIcon
} from 'lucide-react';
import { CategoryType, SortOption, ViewModeType } from './types';

export const CATEGORIES: Record<string, CategoryType> = {
  ALL: 'all',
  WATCHING: 1,
  PLAN: 0,
  COMPLETED: 2,
  DROPPED: 3,
};

export const CATEGORY_ICONS: Record<CategoryType | string, LucideIcon> = {
  'all': Film,
  0: Bookmark,    
  1: Clock, 
  2: CheckCircle, 
  3: XCircle,      
};

export const VIEW_MODES: Record<string, ViewModeType> = {
  LIST: 'list',
};

export const SORT_OPTIONS: SortOption[] = [
  { id: 'name_asc', label: 'By Name (A-Z)' },
  { id: 'name_desc', label: 'By Name (Z-A)' },
  { id: 'date_added', label: 'Date Added' },
  { id: 'chapter_updated', label: 'Chapter Update Date' }
];