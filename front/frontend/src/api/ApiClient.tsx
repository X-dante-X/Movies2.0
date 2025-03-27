import { EmailUsedError, UnauthorizedError } from ".";
import { LoginResponse, verifyResponse } from "../types/responses";
import { saveTokenStorage, removeFromStorage, getAccessToken } from "../services/auth-token.service";

export class ApiClient {
  constructor(private baseUrl: string) {
    console.log(`Initialized ApiClient for ${baseUrl}`);
  }

  private async baseRequest<T>(path: string, options: RequestInit = {}): Promise<T> {
    const token = getAccessToken();
    const { headers, ...otherOptions } = options;

    const res = await fetch(`${this.baseUrl}/${path}`, {
      headers: {
        ...(token && { Authorization: `Bearer ${token}` }),
        "Content-Type": "application/json",
        ...headers,
      },
      ...otherOptions,
    });

    if (res.status === 401) {
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
    const response = await this.baseRequest<LoginResponse>(`login`, {
      method: "POST",
      body: JSON.stringify({ username: email, password }),
    });

    if (response.accessToken) {
      saveTokenStorage(response.accessToken);
    }

    return response;
  }

  async verify(token: string): Promise<verifyResponse> {
    return this.baseRequest<verifyResponse>(`validate`, {
      method: "POST",
      body: JSON.stringify({ token }),
    });
  }

  async register(username: string, email: string, password: string, userstatus: number): Promise<LoginResponse> {
    try {
      const response = await this.baseRequest<LoginResponse>(`register`, {
        method: "POST",
        body: JSON.stringify({
          username,
          email,
          password,
          userstatus,
        }),
      });

      if (response.accessToken) {
        saveTokenStorage(response.accessToken);
      }

      return response;
    } catch (err) {
      if (err instanceof FailedRequestError && err.response.status === 409) {
        throw new EmailUsedError();
      }
      throw err;
    }
  }
  async getNewTokens() {
    const response = await this.baseRequest<LoginResponse>("access-token");

    //if (response.data.accessToken) saveTokenStorage(response.data.accessToken)

    return response;
  }

  async logout() {
    const token = getAccessToken()
    if (!token) {
        console.warn('No access token found');
        return false 
    }
    try {
      const response = await this.baseRequest<boolean>("logout", {
        method: "POST"
      });
      if (response) removeFromStorage()
      return response
    } catch (error) {
      console.error('Logout failed:', error)
      throw error
    }
  }
}

export class FailedRequestError extends Error {
  constructor(public response: Response) {
    super();
  }
}
