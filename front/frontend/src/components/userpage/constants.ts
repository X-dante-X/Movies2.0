import { 
  Bookmark as BookmarkIcon,
  LocalMovies as MoviesIcon,
  WatchLater as WatchLaterIcon,
  Cancel as CancelIcon,
  CheckCircle as CheckCircleIcon,
} from '@mui/icons-material';
import { CategoryType, SortOption, ViewModeType } from './types';
import { SvgIconComponent } from '@mui/icons-material';

export const CATEGORIES: Record<string, CategoryType> = {
  ALL: 'all',
  WATCHING: 1,
  PLAN: 0,
  COMPLETED: 2,
  DROPPED: 3,
};

export const CATEGORY_ICONS: Record<CategoryType | string, SvgIconComponent> = {
  'all': MoviesIcon,
  0: BookmarkIcon,    // Plan to watch
  1: WatchLaterIcon,  // Watching
  2: CheckCircleIcon, // Completed
  3: CancelIcon,      // Dropped
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