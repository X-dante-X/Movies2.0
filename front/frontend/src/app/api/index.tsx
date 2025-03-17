
import { ApiClient } from './ApiClient'

export const apiClient = new ApiClient("https://localhost:7238")

export class UnauthorizedError extends Error {}
export class EmailUsedError extends Error {}