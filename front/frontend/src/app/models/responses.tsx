export interface LoginResponse {
    username: string,
    accessToken: string
    refreshToken: string
    expiration: string
    isAdmin: number
}