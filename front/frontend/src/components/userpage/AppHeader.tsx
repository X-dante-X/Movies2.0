// /components/layout/AppHeader.tsx
import React from 'react';
import { 
  AppBar, 
  Toolbar, 
  Paper, 
  InputBase, 
  IconButton 
} from '@mui/material';
import { Search as SearchIcon } from '@mui/icons-material';

interface AppHeaderProps {
  onSearch?: (query: string) => void;
}

const AppHeader: React.FC<AppHeaderProps> = ({ onSearch }) => {
  return (
    <AppBar position="static" sx={{ bgcolor: '#1e1e1e', boxShadow: 'none' }}>
      <Toolbar>
        <Paper
          component="form"
          sx={{ 
            p: '2px 4px',
            display: 'flex',
            alignItems: 'center',
            width: '100%',
            background: 'linear-gradient(to right, rgba(195, 207, 226, 0.25), rgba(222, 226, 248, 0.25), rgba(226, 226, 242, 0.25))',
            backdropFilter: 'blur(6px)',
            borderRadius: 2,
            color: '#1e1e2f',
            boxShadow: '0 2px 10px rgba(0,0,0,0.1)',
          }}
        >
          <IconButton sx={{ p: '10px', color: 'white' }}>
            <SearchIcon />
          </IconButton>
          <InputBase
            sx={{ ml: 1, flex: 1, color: 'white' }}
            placeholder="Filter by title"
            onChange={(e) => onSearch && onSearch(e.target.value)}
          />
        </Paper>
      </Toolbar>
    </AppBar>
  );
};

export default AppHeader;