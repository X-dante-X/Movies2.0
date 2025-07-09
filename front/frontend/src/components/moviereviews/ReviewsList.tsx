import { MessageCircle } from "lucide-react";
import { m, AnimatePresence } from "framer-motion";
import type { Review } from "../../types/types";
import { ReviewItem } from "./ReviewItem";

interface ReviewsListProps {
  reviews: Review[];
}

export function ReviewsList({ reviews }: ReviewsListProps) {
  return (
    <div className="space-y-4">
      <AnimatePresence>
        {reviews.length === 0 ? (
          <m.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            className="text-center py-12"
          >
            <MessageCircle className="mx-auto text-white/60 mb-4" size={48} />
            <h3 className="text-lg font-medium text-white mb-2">
              No reviews yet
            </h3>
            <p className="text-white/70">
              Be the first to share your thoughts about this movie!
            </p>
          </m.div>
        ) : (
          reviews.map((review, index) => (
            <ReviewItem
              key={`${review.id}-${index}`}
              review={review}
              index={index}
            />
          ))
        )}
      </AnimatePresence>
    </div>

  );
}
