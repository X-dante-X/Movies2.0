import { AxiosError } from 'axios';

export const errorCatch = (error: unknown): string => {
	const axiosError = error as AxiosError<{ message?: string | string[] }>;
	const message = axiosError.response?.data?.message;

	return message
		? Array.isArray(message)
			? message[0]
			: message
		: axiosError.message || 'Unknown error';
};
