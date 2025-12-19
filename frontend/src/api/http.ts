import { ApiError } from '../errors/ApiError';

export async function http<T>(url: string, options?: RequestInit): Promise<T> {
  const response = await fetch(url, { credentials: 'include', ...options });

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
