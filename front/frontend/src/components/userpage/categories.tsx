// /data/constants.js
import { 
    Bookmark as BookmarkIcon,
    LocalMovies as MoviesIcon,
    WatchLater as WatchLaterIcon,
    Cancel as CancelIcon,
    CheckCircle as CheckCircleIcon,
    Favorite as FavoriteIcon,
  } from '@mui/icons-material';
  
  export const CATEGORIES = {
    ALL: 'all',
    WATCHING: 'watching',
    PLAN: 'plan',
    COMPLETED: 'completed',
    DROPPED: 'dropped',
    FAVORITE: 'favorite'
  };
  
  export const CATEGORY_ICONS = {
    [CATEGORIES.ALL]: MoviesIcon,
    [CATEGORIES.WATCHING]: WatchLaterIcon,
    [CATEGORIES.PLAN]: BookmarkIcon,
    [CATEGORIES.COMPLETED]: CheckCircleIcon,
    [CATEGORIES.DROPPED]: CancelIcon,
    [CATEGORIES.FAVORITE]: FavoriteIcon,
  };
  
  export const VIEW_MODES = {
    LIST: 'list',
  };
  
  export const SORT_OPTIONS = [
    { id: 'name_asc', label: 'By Name (A-Z)' },
    { id: 'name_desc', label: 'By Name (Z-A)' },
    { id: 'date_added', label: 'Date Added' },
    { id: 'chapter_updated', label: 'Chapter Update Date' }
  ];