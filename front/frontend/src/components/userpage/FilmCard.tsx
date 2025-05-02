import React from 'react';
import MoreVertIcon from '@mui/icons-material/MoreVert';

interface Film {
  title: string;
  description: string;
  posterPath: string;
  isFavorite: boolean;
  status: number; 
}

interface FilmCardProps {
  film: Film;
}

const FilmCard: React.FC<FilmCardProps> = ({ film }) => {
  const getStatusText = (status: number) => {
    switch (status) {
      case 0:
        return 'Plan to Watch';
      case 1:
        return 'Watching';
      case 2:
        return 'Completed';
      case 3:
        return 'Dropped';
      default:
        return 'Unknown';
    }
  };

  const getCurrentDate = () => {
    const date = new Date();
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();
    return `${day}.${month}.${year}`;
  };

  return (
    <div className="flex mb-2 rounded-lg overflow-hidden bg-gradient-to-b from-[#2e2e4f] to-[#3a3c5a] text-[#e0e0f0] transition-all duration-200 ease-in-out hover:transform hover:scale-102 hover:shadow-lg">
      <div className="w-[100px] h-[140px] flex-shrink-0">
        <img 
          src={`/images/${film.posterPath}`}
          alt={film.title}
          className="w-full h-full object-cover"
        />
      </div>

      <div className="flex flex-col flex-grow">
        <div className="p-4 pb-1 flex-grow">
          <h6 className="text-white text-lg font-medium">
            {film.title}
          </h6>

          <p className="text-blue-400 mt-1 text-sm">
            {getStatusText(film.status)}
          </p>

          <p className="text-[#aaa] mt-1 text-sm">
            {film.description.length > 100 
              ? `${film.description.substring(0, 97)}...` 
              : film.description}
          </p>
        </div>
      </div>

      <div className="flex items-start p-2">
        <div className="text-[#aaa] text-xs">
          Added<br />
          {getCurrentDate()}
        </div>
        <button className="ml-1 text-white p-1">
          <MoreVertIcon fontSize="small" />
        </button>
      </div>
    </div>
  );
};

export default FilmCard;