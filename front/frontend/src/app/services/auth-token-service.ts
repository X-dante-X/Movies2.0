import Cookies from 'js-cookie'

export enum EnumTokens {
  'ACCESS_TOKEN' = 'accessToken',
  'REFRESH_TOKEN' = 'refreshToken'
}

export const getAccessToken = () => {
  const accessToken = Cookies.get(EnumTokens.ACCESS_TOKEN);
  return accessToken || null;
};

export const getRefreshToken = () => {
  const refreshToken = Cookies.get(EnumTokens.REFRESH_TOKEN);
  return refreshToken || null;
};

export const saveTokensStorage = (accessToken: string, refreshToken: string) => {
  Cookies.set(EnumTokens.ACCESS_TOKEN, accessToken, {
    domain: 'localhost',
    sameSite: 'strict',
    expires: 0.01 
  });
  
  Cookies.set(EnumTokens.REFRESH_TOKEN, refreshToken, {
    domain: 'localhost',
    sameSite: 'strict',
    expires: 7 
  });
};

export const removeFromStorage = () => {
  Cookies.remove(EnumTokens.ACCESS_TOKEN);
  Cookies.remove(EnumTokens.REFRESH_TOKEN);
};