
import { ApiClient } from './ApiClient'

export const apiClient = new ApiClient("http://localhost:5000")

export class UnauthorizedError extends Error {}
export class EmailUsedError extends Error {}