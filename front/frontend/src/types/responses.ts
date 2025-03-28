export interface LoginResponse {
    userName: string,
    accessToken: string
    refreshToken: string
    expiration: string
    isAdmin: boolean
}

export interface verifyResponse {
    isAdmin: boolean
}

export interface DecodedToken {
  nameid: string,
  unique_name: string,
  IsAdmin: boolean,
  nbf: number,
  exp: number,
  iat: number
}