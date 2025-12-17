import { getToken, setToken } from './tokenService';
import { ApiError } from '../errors/ApiError';
import { API_BASE_URL } from '../config/apiConfig';
import type { AuthUser } from '../models/users/Users';

interface TokenResponse {
  data: {
    token: string;
  };
  success: boolean;
}
export async function login(email: string, password: string): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/auth/login`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ email, password }),
  });

  if (!response.ok) {
    let message = 'Login failed';

    if (response.status === 400) {
      message = 'Invalid input send to api';
    } else if (response.status === 401) {
      message = 'Invalid credentials';
    } else if (response.status === 404) {
      message = 'No user registered user with that email';
    }

    throw new ApiError(response.status, message);
  }

  const json: TokenResponse = await response.json();
  setToken(json.data.token);
}

interface UserDetailsResponse {
  data: AuthUser;
  success: boolean;
}

export async function getMe(): Promise<UserDetailsResponse> {
  const token = getToken();

  if (token === null) {
    throw new ApiError(0, 'No valid token available.');
  }

  const response = await fetch(`${API_BASE_URL}/users/me`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new ApiError(response.status, 'Failed to fetch user details.');
  }

  const data: UserDetailsResponse = await response.json();
  return data;
}
