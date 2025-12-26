import { API_BASE_URL } from '../config/apiConfig';
import type { AuthUser } from '../models/users/Users';
import { http } from './http';

export interface LoginResponse {
  data: {
    token: string;
  };
  success: boolean;
}

export async function loginUser(
  email: string,
  password: string
): Promise<LoginResponse> {
  return await http<LoginResponse>(`${API_BASE_URL}/auth/login`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password }),
  });
}

export async function logoutUser(): Promise<void> {
  return await http<void>(`${API_BASE_URL}/auth/logout`, {
    method: 'POST',
  });
}

interface UserDetailResponse {
  data: AuthUser;
  success: boolean;
}
export async function getMe(): Promise<UserDetailResponse> {
  return await http<UserDetailResponse>(`${API_BASE_URL}/users/me`);
}
