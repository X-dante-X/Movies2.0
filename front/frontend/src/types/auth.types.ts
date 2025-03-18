export interface IAuthForm {
    username: string;
    password: string;
    email?: string;
  }
  
  export interface IAuthResponse {
    id: number;
    username: string;
    email: string;
    accessToken: string;
    refreshToken: string;
    expiration: string;
    isAdmin: boolean;
  }
  
  export interface ITokenResponse {
    accessToken: string;
    refreshToken: string;
    expiration: string;
  }
  
  export interface IRefreshTokenRequest {
    accessToken: string;
    refreshToken: string;
  }