import { CategoryType } from '../userpage/types';
import { Calendar, Eye, CheckCircle, XCircle } from 'lucide-react';

export const STATUS_OPTIONS = [
  { 
    value: 0 as CategoryType, 
    label: "Plan to Watch", 
    color: "bg-purple-500 hover:bg-purple-600", 
    icon: Calendar 
  },
  { 
    value: 1 as CategoryType, 
    label: "Watching", 
    color: "bg-blue-500 hover:bg-blue-600", 
    icon: Eye 
  },
  { 
    value: 2 as CategoryType, 
    label: "Completed", 
    color: "bg-green-500 hover:bg-green-600", 
    icon: CheckCircle 
  },
  { 
    value: 3 as CategoryType, 
    label: "Dropped", 
    color: "bg-red-500 hover:bg-red-600", 
    icon: XCircle 
  },
];