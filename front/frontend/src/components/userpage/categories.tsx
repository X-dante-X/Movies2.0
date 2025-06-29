// /data/constants.js
import {
  Bookmark,
  Film,
  Clock,
  X,
  CheckCircle,
} from 'lucide-react';

export const CATEGORIES = {
  ALL: 'all',
  WATCHING: 'watching',
  PLAN: 'plan',
  COMPLETED: 'completed',
  DROPPED: 'dropped',
};

export const CATEGORY_ICONS = {
  [CATEGORIES.ALL]: Film,
  [CATEGORIES.WATCHING]: Clock,
  [CATEGORIES.PLAN]: Bookmark,
  [CATEGORIES.COMPLETED]: CheckCircle,
  [CATEGORIES.DROPPED]: X,
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