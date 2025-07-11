import { User } from "lucide-react";
import { m } from "framer-motion";
import type { Review } from "../../types/types";
import { getInitials, getRandomColor } from "../../utils/utils";

interface ReviewItemProps {
  review: Review;
  index: number;
}

export function ReviewItem({ review, index }: ReviewItemProps) {
  return (
      <m.div
      key={`${review.id}-${index}`}
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ delay: index * 0.1 }}
      className="p-8 rounded-2xl shadow-xl backdrop-blur-md bg-white/10 border border-white/20 text-white hover:bg-white/15 transition-all"
    >
      <div className="flex gap-4">
        <div className={`w-12 h-12 rounded-full flex items-center justify-center text-white font-semibold ${getRandomColor(review.userName)}`}>
          {review.userName === "Unknown User" ? (
            <User size={20} />
          ) : (
            getInitials(review.userName)
          )}
        </div>
        <div className="flex-1">
          <div className="flex items-center gap-2 mb-2">
            <h4 className="font-semibold text-white">
              {review.userName}
            </h4>
            <span className="text-white/60 text-sm">•</span>
            <span className="text-white/70 text-sm">
              Recently reviewed
            </span>
          </div>
          <p className="text-white/90 leading-relaxed">
            {review.comment}
          </p>
        </div>
      </div>
    </m.div>
  );
}
