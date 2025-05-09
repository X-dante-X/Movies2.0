"use client";
import { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useAuthStore } from "@/stores/useAuthStore";
import { CategoryType } from "../userpage/types";
import { getUserIdFromToken } from "@/utils/auth";
import { axiosWithAuth } from "@/api/interceptors";
import { STATUS_OPTIONS } from "./constants";
import { Notification } from "./Notification";
import { LucideIcon, Plus, Bookmark, ChevronDown, Loader } from "lucide-react";
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
      console.log(response.data);
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
    
    setIsOpen(!isOpen);
  };
  
  const getButtonText = () => {
    if (currentStatus === undefined || currentStatus === "") {
      return "Add to plans"; 
    }
    return STATUS_OPTIONS.find((opt) => opt.value === currentStatus)?.label;
  };
  
  return (
    <div className="relative mt-4">
      {isOpen && (
        <StatusDropdown
          current={currentStatus}
          onSelect={updateStatus}
        />
      )}
      
      <button
        onClick={handleClick}
        disabled={isPending}
        className="bg-gray-900 hover:bg-gray-800 text-orange-500 font-normal rounded px-4 py-2 shadow flex items-center justify-between space-x-2 transition-all duration-200 disabled:opacity-70 w-56 border border-gray-800">
        {isPending ? (
          <Loader className="animate-spin h-4 w-4 text-orange-500" />
        ) : (
          <>
            <div className="flex items-center">
              {currentStatus !== undefined ? (
                <Bookmark className="w-4 h-4 mr-2 text-orange-500" />
              ) : (
                <Plus className="w-4 h-4 mr-2 text-orange-500" />
              )}
              <span className="text-sm">{getButtonText()}</span>
            </div>
            <ChevronDown className={`w-4 h-4 transition-transform text-orange-500 ${isOpen ? "rotate-180" : ""}`} />
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
  current: CategoryType | undefined;  // Made this optional to fix the bug
  onSelect: (status: CategoryType) => void;
}
function StatusDropdown({ current, onSelect }: StatusDropdownProps) {
  return (
    <div className="absolute z-20 bottom-full mb-3 w-64 right-0 rounded-xl shadow-xl bg-gray-900 border border-gray-800">
      {STATUS_OPTIONS.map(({ value, label, icon }) => {
        const Icon = icon as LucideIcon;
        return (
          <button
            key={value}
            onClick={() => onSelect(value)}
            className={`w-full flex items-center px-4 py-3 hover:bg-gray-800 text-orange-500 transition duration-200 ${
              value === current ? "bg-gray-800" : ""
            }`}>
            <div className={`w-8 h-8 rounded-full flex items-center justify-center shadow-sm bg-gray-800 text-orange-500`}>
              <Icon size={16} />
            </div>
            <span className="ml-3 text-left text-sm">{label}</span>
          </button>
        );
      })}
    </div>
  );
}