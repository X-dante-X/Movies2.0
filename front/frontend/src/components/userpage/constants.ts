import { 
    Bookmark as BookmarkIcon,
    LocalMovies as MoviesIcon,
    WatchLater as WatchLaterIcon,
    Cancel as CancelIcon,
    CheckCircle as CheckCircleIcon,
    Favorite as FavoriteIcon,
  } from '@mui/icons-material';
  import { CategoryType, SortOption, ViewModeType } from './types';
  import { SvgIconComponent } from '@mui/icons-material';
  
  export const CATEGORIES: Record<string, CategoryType> = {
    ALL: 0,
    WATCHING: 1,
    PLAN: 2,
    COMPLETED: 3,
    DROPPED: 4,
    FAVORITE: 5
  };
  
  export const CATEGORY_ICONS: Record<CategoryType, SvgIconComponent> = {
    0: MoviesIcon,
    1: WatchLaterIcon,
    2: BookmarkIcon,
    3: CheckCircleIcon,
    4: CancelIcon,
    5: FavoriteIcon,
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