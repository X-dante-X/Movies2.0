
import { ApiClient } from './ApiClient'

export const apiClient = new ApiClient("http://localhost/auth");

export class UnauthorizedError extends Error {}
export class EmailUsedError extends Error {}