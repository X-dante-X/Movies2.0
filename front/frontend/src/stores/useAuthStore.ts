import { create } from "zustand";
import { persist } from "zustand/middleware";
import { IAuthResponse } from "@/types/auth.types";

interface AuthState {
  user: Omit<IAuthResponse, "accessToken" | "refreshToken" | "expiration"> | null;
  setUser: (user: Omit<IAuthResponse, "accessToken" | "refreshToken" | "expiration">) => void;
  clearUser: () => void;
}

// Zustand store for authentication state
export const useAuthStore = create<AuthState>()(
  persist(
    (set) => ({
      user: null,
      setUser: (user) => set({ user }),
      clearUser: () => set({ user: null }),
    }),
    {
      name: "authUser",
    }
  )
);
