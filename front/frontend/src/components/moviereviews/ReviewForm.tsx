import { useState } from "react";
import { Send } from "lucide-react";
import { m } from "framer-motion";
import type { Review } from "../../types/types";

interface ReviewFormProps {
  movieId: number;
  onReviewSubmitted: (review: Review) => void;
}

export function ReviewForm({ movieId, onReviewSubmitted }: ReviewFormProps) {
  const [newComment, setNewComment] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!newComment.trim()) return;

    setIsSubmitting(true);
    try {
      const response = await fetch(`http://localhost/favorites/review`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        // has to be changed
        body: JSON.stringify({
          userid: "1",
          movieId: movieId,
          rating: "10",
          comment: newComment.trim(),
        }),
      });

      if (response.ok) {
        const newReview = await response.json();
        onReviewSubmitted(newReview);
        setNewComment("");
      }
    } catch (error) {
      console.error("Failed to submit review:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="p-8 rounded-2xl shadow-xl backdrop-blur-md bg-white/10 border border-white/20 text-white mb-8">

      <form onSubmit={handleSubmit} className="space-y-4">
        <div className="relative">
          <textarea
            value={newComment}
            onChange={(e) => setNewComment(e.target.value)}
            placeholder="What did you think about this movie?"
            className="w-full p-4 bg-white/5 border border-white/20 rounded-lg text-white placeholder-white/60 focus:ring-2 focus:ring-blue-500/50 focus:border-blue-500/50 resize-none transition-colors backdrop-blur-sm"
            rows={4}
            maxLength={500}
          />
          <div className="absolute bottom-2 right-2 text-xs text-white/60">
            {newComment.length}/500
          </div>
        </div>
        <div className="flex justify-between items-center">
          <m.button
            type="submit"
            disabled={!newComment.trim() || isSubmitting}
            className="flex items-center gap-2 bg-blue-500 text-white px-6 py-2 rounded-lg hover:bg-blue-600 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
            whileHover={{ scale: 1.02 }}
            whileTap={{ scale: 0.98 }}
          >
            {isSubmitting ? (
              <>
                <div className="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin"></div>
                Posting...
              </>
            ) : (
              <>
                <Send size={16} />
                Post Review
              </>
            )}
          </m.button>
        </div>
      </form>
    </div>

  );
}
