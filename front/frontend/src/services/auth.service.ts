import { IAuthLoginForm, IAuthRegisterForm, IAuthResponse } from '@/types/auth.types'

import { axiosClassic } from '@/api/interceptors'

import { removeFromStorage, saveTokenStorage } from './auth-token.service'

export const authService = {
	async login(data: IAuthLoginForm) {
		const response = await axiosClassic.post<IAuthResponse>(
			"/login",
			data
		)

		if (response.data.accessToken) saveTokenStorage(response.data.accessToken)

		return response
	},

	async register(data: IAuthRegisterForm) {
		const response = await axiosClassic.post<IAuthResponse>(
			"/register",
			data
		)

		if (response.data.accessToken) saveTokenStorage(response.data.accessToken)

		return response
	},

	async getNewTokens() {
		const response = await axiosClassic.post<IAuthResponse>(
			'/access-token'
		)

		if (response.data.accessToken) saveTokenStorage(response.data.accessToken)

		return response
	},

	async logout() {
		const response = await axiosClassic.post<boolean>('/logout')

		if (response.data) removeFromStorage()

		return response
	}
}
