import { createContext, useEffect, useState } from 'react';
import { getMe, loginUser, logoutUser } from '../api/authService';
import type { AuthUser } from '../models/users/Users';
import { ApiError } from '../errors/ApiError';
import { authEvents } from '../api/authEvents';

export interface AuthContextValue {
  user: AuthUser | null;
  isAuthenticated: boolean;
  authChecked: boolean;
  isLoading: boolean;
  errorMessage: string | null;
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
}

interface AuthContextProviderProps {
  children: React.ReactNode;
}

export const AuthContext = createContext<AuthContextValue | undefined>(
  undefined
);

export function AuthProvider({ children }: AuthContextProviderProps) {
  const [user, setUser] = useState<AuthUser | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [authChecked, setAuthChecked] = useState(false);

  const isAuthenticated = user !== null;

  useEffect(() => {
    console.log('checking auth status on mount');
    // try to fetch user details for authenticated users on mount
    authCheck();
  }, []);

  useEffect(() => {
    // subscribe to unauthorized events to set auth state for 401 responses during API calls
    const unsubscribe = authEvents.onUnauthorized(() => {
      console.log('handling unauthorized event in AuthContext');
      setUser(null);
    });

    return unsubscribe;
  }, []);

  async function authCheck(): Promise<void> {
    try {
      const response = await getMe();
      setUser(response.data);
    } catch (error) {
      setUser(null);
    } finally {
      setAuthChecked(true);
    }
  }

  async function login(email: string, password: string): Promise<void> {
    setErrorMessage(null);
    setIsLoading(true);

    try {
      await loginUser(email, password);
      const userDetails = await getMe();
      setUser(userDetails.data);
    } catch (error) {
      if (error instanceof ApiError) {
        setErrorMessage(error.message);
      } else {
        setErrorMessage('An unexpected error occurred.');
      }
    } finally {
      setIsLoading(false);
    }
  }

  async function logout(): Promise<void> {
    try {
      await logoutUser();
      setUser(null);
    } catch (error) {
      console.log('caught this error in the logout context function', error);
      if (error instanceof ApiError) {
        setErrorMessage(error.message);
      } else {
        setErrorMessage('An unexpected error occurred during logout.');
      }
    }
  }

  const contextValue: AuthContextValue = {
    user,
    isAuthenticated,
    authChecked,
    isLoading,
    errorMessage,
    login,
    logout,
  };

  return (
    <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>
  );
}
