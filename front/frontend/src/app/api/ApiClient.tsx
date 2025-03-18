import { EmailUsedError, UnauthorizedError } from '.';
import { LoginResponse } from '../models/responses';
import { useLoginStore } from '../stores/userStore';

export class ApiClient {
    constructor(private baseUrl: string) {
        console.log(`Initialized ApiClient for ${baseUrl}`);
    }

    private async baseRequest<T>(path: string, options: RequestInit = {}): Promise<T> {
        const token = useLoginStore.getState().userData?.token;
        const { headers, ...otherOptions } = options;

        const res = await fetch(`${this.baseUrl}/${path}`, {
            headers: {
                ...(token && { Authorization: `Bearer ${token}` }),
                'Content-Type': 'application/json',
                ...headers,
            },
            ...otherOptions,
        });

        if (res.status === 401) {
            if (token !== undefined) {
                console.log('Unauthorized, logging out');
                useLoginStore.getState().logOut();
            }
            throw new UnauthorizedError();
        } else if (!res.ok) {
            throw new FailedRequestError(res);
        }

        try {
            const json = await res.json();
            return json as T;
        } catch {
            return null as unknown as T;
        }
    }

    async logIn(email: string, password: string): Promise<LoginResponse> {
        return this.baseRequest<LoginResponse>(`api/auth/login`, {
            method: 'POST',
            body: JSON.stringify({ username: email, password }),
        });
    }

    async register(
        username: string,
        email: string,
        password: string,
        userstatus: number
    ): Promise<LoginResponse> {
        try {
            return await this.baseRequest<LoginResponse>(`api/auth/register`, {
                method: 'POST',
                body: JSON.stringify({
                    username,
                    email,
                    password,
                    userstatus
                }),
            });
        } catch (err) {
            if (err instanceof FailedRequestError && err.response.status === 409) {
                throw new EmailUsedError();
            }
            throw err;
        }
    }
}

export class FailedRequestError extends Error {
    constructor(public response: Response) {
        super();
    }
}