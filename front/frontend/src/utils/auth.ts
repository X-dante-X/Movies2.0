import { jwtDecode } from 'jwt-decode';
import { getAccessToken } from '@/services/auth-token.service';

export const getUserIdFromToken = () => {
  try {
    const token = getAccessToken();
    if (!token) {
      return null;
    }
    const decodedToken = jwtDecode(token) as { nameid: string };
    return decodedToken.nameid;
  } catch (err) {
    console.error("Error decoding token:", err);
    return null;
  }
};