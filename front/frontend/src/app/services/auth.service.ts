import { IAuthForm, IAuthResponse, ITokenResponse, IRefreshTokenRequest } from '@/types/auth.types';
import { axiosClassic, instance } from '@/api/interceptors';
import { getAccessToken, getRefreshToken, removeFromStorage, saveTokensStorage } from './auth-token-service';

export const authService = {
  async register(data: IAuthForm) {
    const response = await axiosClassic.post<IAuthResponse>(
      '/api/auth/register',
      data
    );
    
    if (response.data.accessToken && response.data.refreshToken) {
      saveTokensStorage(response.data.accessToken, response.data.refreshToken);
    }
    
    return response;
  },
  
  async login(data: IAuthForm) {
    const response = await axiosClassic.post<IAuthResponse>(
      '/api/auth/login',
      data
    );
    
    if (response.data.accessToken && response.data.refreshToken) {
      saveTokensStorage(response.data.accessToken, response.data.refreshToken);
    }
    
    return response;
  },
  
  async getNewTokens() {
    const accessToken = getAccessToken();
    const refreshToken = getRefreshToken();
    
    if (!accessToken || !refreshToken) {
      throw new Error('No tokens found');
    }
    
    const response = await axiosClassic.post<ITokenResponse>(
      '/api/auth/refresh-token',
      {
        accessToken,
        refreshToken
      } as IRefreshTokenRequest
    );
    
    if (response.data.accessToken && response.data.refreshToken) {
      saveTokensStorage(response.data.accessToken, response.data.refreshToken);
    }
    
    return response;
  },
  
  async logout() {
    const response = await instance.post<{ message: string }>(
      '/api/auth/revoke-token'
    );
    
    if (response.status === 200) {
      removeFromStorage();
    }
    
    return response;
  },
  
  async checkAuth() {
    const accessToken = getAccessToken();
    
    if (!accessToken) {
      return null;
    }
    
    try {
      // Try to access a protected route to check if token is valid
      // This could be a specific endpoint like /api/users/me
      // For now, we'll just return the token (in a real app, verify with backend)
      return { accessToken };
    } catch (error) {
      return null;
    }
  }
};
