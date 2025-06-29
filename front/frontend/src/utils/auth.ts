import { decodeJwt } from 'jose';
import { getAccessToken } from '@/services/auth-token.service';

export const getUserIdFromToken = () => {
  try {
    const token = getAccessToken();
    if (!token) {
      return null;
    }

    const decodedToken = decodeJwt(token) as { nameid?: string };
    return decodedToken.nameid ?? null;
  } catch (err) {
    console.error('Error decoding token:', err);
    return null;
  }
};