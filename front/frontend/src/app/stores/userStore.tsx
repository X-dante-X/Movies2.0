import { useMutation, useQueryClient } from '@tanstack/react-query';
import { apiClient } from '../api/index';
import { getAccessToken } from '../services/auth-token.service';

export type UserData = {
  username: string;
  isAdmin: boolean;
};

export function useAuthToken(): string | undefined {
  return getAccessToken() || undefined;
}

export function useLogin() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async ({ email, password }: { email: string; password: string }) => {
      const response = await apiClient.logIn(email, password);
      return response;
    },
    onSuccess: () => {
      queryClient.invalidateQueries();
    }
  });
}

export function useRegister() {
  const queryClient = useQueryClient();

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
      const response = await apiClient.register( 
        username, 
        email, 
        password,
        userstatus 
      );
      return response;
    },
    onSuccess: () => {
      queryClient.invalidateQueries();
    }
  });
}

export async function verifyUser(): Promise<boolean> {
  const token = getAccessToken();
  if (!token) return false;

  try {
    const response = await apiClient.verify(token);
    return response?.isAdmin || false;
  } catch (error) {
    console.error("Error verifying user:", error);
    return false;
  }
}

export function useLogout() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async () => {
      return await apiClient.logout();
    },
    onSuccess: () => {
      queryClient.clear(); // Clear all queries on logout
    }
  });
}