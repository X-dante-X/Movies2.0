import React from 'react';
import { 
  Box, 
  Paper, 
  Typography, 
  Divider, 
  List, 
  ListItem, 
  ListItemButton, 
  ListItemIcon, 
  ListItemText, 
  Badge 
} from '@mui/material';
import { ViewList as ViewListIcon } from '@mui/icons-material';
import { CATEGORIES, CATEGORY_ICONS, SORT_OPTIONS } from './constants';
import { CategoryCounts, CategoryType, ViewModeType } from './types';

// Helper function to map from numeric category type to string key
const getCategoryKey = (category: CategoryType): keyof CategoryCounts => {
  switch(category) {
    case 0: return 'all';
    case 1: return 'watching';
    case 2: return 'plan';
    case 3: return 'completed';
    case 4: return 'dropped';
    case 5: return 'favorite';
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

const Sidebar: React.FC<SidebarProps> = ({ 
  selectedCategory, 
  onCategoryChange, 
  categoryCounts, 
  viewMode, 
  onViewModeChange 
}) => {
  return (
    <Paper 
      elevation={3} 
      sx={{ 
        width: 240,
        background: '#2e2e4f',
        color: '#e0e0f0',
        borderRight: '1px solid #444',
        display: { xs: 'none', sm: 'block' },
        '& .MuiListItemIcon-root': {
          color: '#a78bfa',
        },
        boxShadow: '4px 0 10px rgba(0,0,0,0.2)',
      }}
    >
      <Box sx={{ p: 2 }}>
        <Typography variant="h6" component="div">
          Lists
        </Typography>
      </Box>
      <Divider sx={{ bgcolor: '#333' }} />
      <List>
        {Object.entries(CATEGORIES).map(([key, value]) => {
          const numericValue = Number(value) as CategoryType;
          const categoryKey = getCategoryKey(numericValue);
          const IconComponent = CATEGORY_ICONS[value];
          
          return (
            <ListItem key={value} disablePadding>
              <ListItemButton
                selected={selectedCategory === numericValue}
                onClick={() => onCategoryChange(numericValue)}
                sx={{ 
                  '&.Mui-selected': { bgcolor: '#333' },
                  '&:hover': { bgcolor: '#333' }
                }}
              >
                <ListItemIcon sx={{ color: 'white' }}>
                  <IconComponent />
                </ListItemIcon>
                <ListItemText primary={key.charAt(0) + key.slice(1).toLowerCase()} />
                <Badge badgeContent={categoryCounts[categoryKey]} color="primary" />
              </ListItemButton>
            </ListItem>
          );
        })}
      </List>

      <Divider sx={{ bgcolor: '#333', mt: 2 }} />
      <Box sx={{ p: 2 }}>
        <Typography variant="subtitle2" sx={{ color: '#aaa' }}>
          View
        </Typography>
        <List>
          <ListItem disablePadding>
            <ListItemButton
              selected={viewMode === 'list'}
              onClick={() => onViewModeChange('list')}
              sx={{ 
                '&.Mui-selected': { bgcolor: '#333' },
                '&:hover': { bgcolor: '#333' }
              }}
            >
              <ListItemIcon sx={{ color: 'white' }}>
                <ViewListIcon />
              </ListItemIcon>
              <ListItemText primary="List" />
            </ListItemButton>
          </ListItem>
        </List>
      </Box>

      <Divider sx={{ bgcolor: '#333' }} />
      <Box sx={{ p: 2 }}>
        <Typography variant="subtitle2" sx={{ color: '#aaa' }}>
          Sort By
        </Typography>
        <List>
          {SORT_OPTIONS.map(option => (
            <ListItem key={option.id} disablePadding>
              <ListItemButton sx={{ '&:hover': { bgcolor: '#333' } }}>
                <ListItemText primary={option.label} />
              </ListItemButton>
            </ListItem>
          ))}
        </List>
      </Box>
    </Paper>
  );
};

export default Sidebar;