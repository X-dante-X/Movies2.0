import { MessageCircle } from "lucide-react";

interface ReviewsHeaderProps {
  movieTitle: string;
  reviewCount: number;
}

export function ReviewsHeader({ movieTitle, reviewCount }: ReviewsHeaderProps) {
  return (
    <div className="flex items-center gap-3 mb-8">
      <MessageCircle className="text-blue-400" size={28} />
      <h2 className="text-3xl font-semibold text-white">
        Reviews for {movieTitle}
      </h2>
      <span className="bg-white/10 backdrop-blur-md border border-white/20 text-white px-3 py-1 rounded-full text-sm font-medium">
        {reviewCount} review{reviewCount !== 1 ? "s" : ""}
      </span>
    </div>

  );
}
