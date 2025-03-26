export interface IAuthLoginForm {
  userName: string;
  password: string;
}

export interface IAuthRegisterForm {
  username: string;
  password: string;
  email: string;
}

export interface IAuthResponse {
  userName: string,
  accessToken: string
  refreshToken: string
  expiration: string
  isAdmin: boolean
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