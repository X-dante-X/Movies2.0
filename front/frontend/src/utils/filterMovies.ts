import { Film, CategoryType, CategoryCounts } from '../components/userpage/types';

export const getFilteredFilms = (films: Film[], selectedCategory: CategoryType): Film[] => {
  return selectedCategory === 0 
    ? films 
    : films.filter(film => film.status === selectedCategory);
};

export const getCategoryCounts = (films: Film[]): CategoryCounts => {
  return {
    all: films.length,
    watching: films.filter(film => film.status === 1).length,
    plan: films.filter(film => film.status === 2).length,
    completed: films.filter(film => film.status === 2).length,
    dropped: films.filter(film => film.status === 3).length,
    favorite: films.filter(film => film.status === 4).length,
  };
};