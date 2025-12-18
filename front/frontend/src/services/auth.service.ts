import { IAuthResponse, IAuthLoginForm, IAuthRegisterForm } from '@/types/auth.types'
import { axiosClassic } from '@/api/interceptors'
import { removeFromStorage, saveTokenStorage } from './auth-token.service'
import { useAuthStore } from '@/stores/useAuthStore'


export const authService = {
	// Enables user to log in to their account
	async login(data: IAuthLoginForm) {
		const response = await axiosClassic.post<IAuthResponse>("/login", data)
		if (response.data) {
			useAuthStore.getState().setUser({
				userName: response.data.userName,
				isAdmin: response.data.isAdmin
			})
			if (response.data.accessToken) saveTokenStorage(response.data.accessToken)
		}
		return response
	},
	// Enables user to register their account by providing necessary data
	async register(data: IAuthRegisterForm) {
		const response = await axiosClassic.post<IAuthResponse>("/register", data);
		console.log(data)
		console.log(response.data)
		console.log(response)
		if (response.data) {
			useAuthStore.getState().setUser({
				userName: response.data.userName,
				isAdmin: response.data.isAdmin
			})
			if (response.data.accessToken) saveTokenStorage(response.data.accessToken)
		}
		return response
	},
	// Gets new access tokens
	async getNewTokens() {
		const response = await axiosClassic.post<IAuthResponse>(
			'/access-token'
		)

		if (response.data.accessToken) saveTokenStorage(response.data.accessToken)

		return response
	},
	// logs the user out and cuts the session.
	async logout() {
		const response = await axiosClassic.post<boolean>("/logout")
		if (response.data) {
			useAuthStore.getState().clearUser()
			removeFromStorage()
		}

		return response
	}
}
