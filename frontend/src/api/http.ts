import { ApiError } from '../errors/ApiError';
import { authEvents } from './authEvents';

export async function http<T>(url: string, options?: RequestInit): Promise<T> {
  const response = await fetch(url, {
    credentials: 'include',
    headers: { 'Content-Type': 'application/json' },
    ...options,
  });

  // On unauthorized response status, notify subscribers
  if (response.status === 401) {
    authEvents.emitUnauthorized();
  }

  if (!response.ok) {
    const message = mapStatusToMessage(response.status);
    throw new ApiError(response.status, message);
  }

  const text = await response.text();

  if (!text) {
    return undefined as T;
  }

  return JSON.parse(text) as T;
}

function mapStatusToMessage(statusCode: number): string {
  switch (statusCode) {
    case 400:
      return 'Client error.';
    case 401:
      return 'Not authenticated.';
    case 403:
      return 'Access denied.';
    case 404:
      return 'Not found.';
    case 500:
      return 'Server error.';
    default:
      return 'Unknown error.';
  }
}
