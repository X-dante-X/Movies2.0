"use client";
import { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useAuthStore } from "@/stores/useAuthStore";
import { CategoryType } from "../userpage/types";
import { getUserIdFromToken } from "@/utils/auth";
import { axiosWithAuth } from "@/api/interceptors";
import { STATUS_OPTIONS } from "./constants";
import { Notification } from "./Notification";
import { LucideIcon, Star, ChevronUp, Loader } from "lucide-react";

interface FavoriteButtonProps {
  movieId: number;
}

export function FavoriteButton({ movieId }: FavoriteButtonProps) {
  const { user } = useAuthStore();
  const userId = getUserIdFromToken();
  const [isOpen, setIsOpen] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState(false);
  const queryClient = useQueryClient();

  const { data: currentStatus } = useQuery({
    queryKey: ["watchStatus", movieId],
    queryFn: async () => {
      const response = await axiosWithAuth.get(`http://localhost/favorites/${movieId}`);
      return response.data as CategoryType | undefined;
    },
    enabled: !!user,
  });

  const { mutate: updateStatus, isPending } = useMutation({
    mutationFn: async (status: CategoryType) => {
      const response = await axiosWithAuth.post("http://localhost/favorites", {
        UserId: userId,
        MovieId: movieId,
        IsFavorite: true,
        Status: status,
      });
      return response.data;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["watchStatus", movieId] });
      setSuccess(true);
      setIsOpen(false);
      setTimeout(() => setSuccess(false), 3000);
    },
    onError: (err) => {
      setError(err instanceof Error ? err.message : "An error occurred");
      setTimeout(() => setError(null), 3000);
    },
  });

  const handleClick = () => {
    if (!user) {
      setError("Please log in to manage your list.");
      return;
    }

    if (!currentStatus) {
      updateStatus(0); // Plan to Watch
    } else {
      setIsOpen(!isOpen);
    }
  };

  return (
    <div className="relative mt-4">
      {isOpen && currentStatus !== undefined && (
        <StatusDropdown
          current={currentStatus}
          onSelect={updateStatus}
        />
      )}

      <button
        onClick={handleClick}
        disabled={isPending}
        className="bg-purple-600 hover:bg-purple-700 text-white font-bold rounded-full px-6 py-3 shadow-lg flex items-center justify-center space-x-2 transition-all duration-300 transform hover:scale-105 disabled:opacity-70 relative overflow-hidden group">
        {isPending ? (
          <Loader className="animate-spin h-5 w-5" />
        ) : (
          <>
            <Star
              className="w-5 h-5"
              fill="currentColor"
            />
            <span className="relative z-10 tracking-wide">
              {currentStatus === undefined ? "Add to Plan" : STATUS_OPTIONS.find((opt) => opt.value === currentStatus)?.label}
            </span>
            <ChevronUp className={`w-5 h-5 transition-transform ${isOpen ? "rotate-180" : ""}`} />
          </>
        )}
      </button>

      {error && (
        <Notification
          type="error"
          message={error}
        />
      )}
      {success && (
        <Notification
          type="success"
          message="Status updated!"
        />
      )}
    </div>
  );
}

interface StatusDropdownProps {
  current: CategoryType;
  onSelect: (status: CategoryType) => void;
}

function StatusDropdown({ current, onSelect }: StatusDropdownProps) {
  return (
    <div className="absolute z-20 bottom-full mb-3 w-64 right-0 rounded-xl shadow-xl bg-gray-900/90 backdrop-blur-xl border border-purple-400/20">
      {STATUS_OPTIONS.map(({ value, label, color, icon }) => {
        const Icon = icon as LucideIcon;
        return (
          <button
            key={value}
            onClick={() => onSelect(value)}
            className={`w-full flex items-center px-5 py-4 hover:bg-purple-900/40 text-white transition duration-200 ${
              value === current ? "bg-purple-800/30" : ""
            }`}>
            <div className={`w-10 h-10 rounded-full flex items-center justify-center shadow-md ${color.split(" ")[0]}`}>
              <Icon size={20} />
            </div>
            <span className="ml-4 text-left">{label}</span>
          </button>
        );
      })}
    </div>
  );
}
