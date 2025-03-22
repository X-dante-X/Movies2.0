export interface LoginResponse {
    userName: string,
    accessToken: string
    refreshToken: string
    expiration: string
    isAdmin: number
}

export interface verifyResponse {
    isAdmin: boolean
}