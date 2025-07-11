"use client";

import { useState, useEffect } from "react";
import type { Review, MovieReviewsProps } from "../../types/types";
import { ReviewsHeader } from "./ReviewHeader";
import { ReviewForm } from "./ReviewForm";
import { ReviewsList } from "./ReviewsList";
import { LoadingState } from "./LoadingState";

export function MovieReviews({ movieId, movieTitle }: MovieReviewsProps) {
  const [reviews, setReviews] = useState<Review[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchReviews = async () => {
      try {
        const response = await fetch(`http://localhost/favorites/movieReviews/${movieId}`);
        console.log(response)
        if (response.ok) {
          const data = await response.json();
          setReviews(data);
        }
      } catch (error) {
        console.error("Failed to fetch reviews:", error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchReviews();
  }, [movieId]);

  const handleReviewSubmitted = (newReview: Review) => {
    setReviews(prev => [newReview, ...prev]);
  };

  if (isLoading) {
    return <LoadingState />;
  }

  return (
    <div className="w-full max-w-4xl mx-auto p-6 bg-gradient-to-br from-slate-900 via-slate-800 to-slate-900 min-h-screen">
      <ReviewsHeader movieTitle={movieTitle} reviewCount={reviews.length} />
      
      <ReviewForm 
        movieId={movieId} 
        onReviewSubmitted={handleReviewSubmitted} 
      />
      
      <ReviewsList reviews={reviews} />

      {reviews.length > 0 && (
        <div className="text-center mt-8">
          <button className="text-blue-400 hover:text-blue-300 font-medium transition-colors">
            Load more reviews
          </button>
        </div>
      )}
    </div>

  );
}
