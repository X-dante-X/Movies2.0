import { create } from "zustand";
import { LoginResponse } from "../models/responses";
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { ApiClient } from '../api/ApiClient';
import Cookies from "js-cookie";

const userDataStorageKey = "userData";

export type UserData = {
  username: string;
  token: string;
  isAdmin: boolean;
  refreshToken: string
};

type LoginStore = {
  userData?: UserData;
  isLoggedIn: () => boolean;
  logIn: (loginResponse: LoginResponse) => void;
  logOut: () => void;
};

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

  logIn(loginResponse: LoginResponse) {
    const userData = loginResponseToUserData(loginResponse);
    set({ userData });
    Cookies.set('token', userData.token, {
      path: '/',
      secure: true,
      sameSite: 'none',
      expires: 7
  })
    if (typeof window !== "undefined") {
      localStorage.setItem(userDataStorageKey, JSON.stringify(userData));
    }
  },

  logOut() {
    set({ userData: undefined });

    if (typeof window !== "undefined") {
      localStorage.removeItem(userDataStorageKey);
    }
  },
}));

function loginResponseToUserData(loginResponse: LoginResponse): UserData {
  return {
    username: loginResponse.userName,
    isAdmin: loginResponse.isAdmin,
    token: loginResponse.accessToken,
    refreshToken: loginResponse.refreshToken
  };
}

export async function verifyUser(apiClient: ApiClient): Promise<boolean> {
  const data = useLoginStore.getState().userData;
  const username = data?.username ?? " ";
  try {
    console.log(username)
    const response = await apiClient.verify(username);
    if (response?.isAdmin) {
      return true;
    }
    return false;
  } catch (error) {
    console.error("Error verifying user:", error);
    return false;
  }
}

export function useAuthToken(): string | undefined {
  return useLoginStore(state => state.userData?.token);
}

export function useLogin(apiClient: ApiClient) {
  const queryClient = useQueryClient();
  const loginStore = useLoginStore();

  return useMutation({
      mutationFn: async ({ email, password }: { email: string; password: string }) => {
          return await apiClient.logIn(email, password);
      },
      onSuccess: (data) => {
          loginStore.logIn(data);
          queryClient.invalidateQueries();
      }
  });
}

export function useRegister(apiClient: ApiClient) {
  const queryClient = useQueryClient();
  const loginStore = useLoginStore();

  return useMutation({
      mutationFn: async ({ 
          username, 
          email, 
          password, 
          userstatus 
      }: { 
          username: string; 
          email: string; 
          password: string; 
          userstatus: number 
      }) => {
          const response = await apiClient.register(username, email, password, userstatus);
          return response;
      },
      onSuccess: (data) => {
          loginStore.logIn(data);
          // Invalidate queries that might depend on authentication status
          queryClient.invalidateQueries();
      }
  });
}