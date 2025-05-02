import { Star, ChevronUp, Loader } from 'lucide-react';

interface ButtonContentProps {
  isLoading: boolean;
  isOpen: boolean;
}

export default function ButtonContent({ isLoading, isOpen }: ButtonContentProps) {
  if (isLoading) {
    return <Loader className="animate-spin h-5 w-5 text-white" />;
  }

  return (
    <>
      <div className="absolute inset-0 bg-gradient-to-r from-purple-400/20 to-pink-500/20 opacity-0 group-hover:opacity-100 transition-opacity duration-500"></div>
      <div className="absolute -inset-x-full top-5 bottom-0 w-1/2 transform -skew-x-12 bg-white opacity-10 animate-shimmer"></div>
      <Star className="w-5 h-5" fill="currentColor" />
      <span className="relative z-10 tracking-wide">Add to Favorites</span>
      <ChevronUp className={`w-5 h-5 transition-transform duration-300 ${isOpen ? 'rotate-180 transform' : ''}`} />
    </>
  );
}