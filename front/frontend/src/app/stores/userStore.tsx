import { create } from "zustand";
import { apiClient, EmailUsedError, UnauthorizedError } from "../api";
import { LoginResponse } from "../models/responses";

const userDataStorageKey = "userData";

export type UserData = {
  token: string;
  isAdmin: boolean;
};

type LoginStore = {
  userData?: UserData;
  isLoggedIn: () => boolean;
  logIn: (email: string, password: string) => Promise<boolean>;
  register: (
    username: string,
    email: string,
    password: string,
    userstatus: number
  ) => Promise<boolean>;
  logOut: () => void;
};

// ✅ Ensure localStorage is accessed only on the client
const getStoredUserData = () => {
  if (typeof window !== "undefined") {
    const storedData = localStorage.getItem(userDataStorageKey);
    return storedData ? JSON.parse(storedData) : undefined;
  }
  return undefined;
};

export const useLoginStore = create<LoginStore>((set, get) => ({
  userData: getStoredUserData(),

  isLoggedIn() {
    return get().userData !== undefined;
  },

  async logIn(email, password) {
    try {
      const loginResponse = await apiClient.logIn(email, password);
      const userData = loginResponseToUserData(loginResponse);
      
      set({ userData });

      // ✅ Store user data only on the client
      if (typeof window !== "undefined") {
        localStorage.setItem(userDataStorageKey, JSON.stringify(userData));
      }

      return true;
    } catch (err) {
      if (err instanceof UnauthorizedError) {
        return false;
      }
      throw err;
    }
  },

  async register(username, email, password, userstatus) {
    try {
      await apiClient.register(username, email, password, userstatus);
      return await get().logIn(username, password);
    } catch (err) {
      if (err instanceof EmailUsedError) {
        return false;
      }
      throw err;
    }
  },

  logOut() {
    set({ userData: undefined });

    // ✅ Remove user data only on the client
    if (typeof window !== "undefined") {
      localStorage.removeItem(userDataStorageKey);
    }
  },
}));

function loginResponseToUserData(loginResponse: LoginResponse): UserData {
  return {
    isAdmin: loginResponse.isAdmin === 1,
    token: loginResponse.accessToken,
  };
}
