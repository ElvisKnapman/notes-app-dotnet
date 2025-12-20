import { createContext, useEffect, useState } from 'react';
import { getMe, loginUser, logoutUser } from '../api/authService';
import type { AuthUser } from '../models/users/Users';
import { ApiError } from '../errors/ApiError';

export interface AuthContextValue {
  user: AuthUser | null;
  isAuthenticated: boolean;
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
  const [attemptedLogin, setAttemptedLogin] = useState(false);

  const isAuthenticated = user !== null;

  useEffect(() => {
    console.log('user state changed', user);
  }, [user]);

  async function login(email: string, password: string): Promise<void> {
    setErrorMessage(null);
    setIsLoading(true);

    try {
      const token = (await loginUser(email, password)).data.token;
      const userDetails = await getMe(token);
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
    console.log('called the dang logout function');
    try {
      await logoutUser();
      console.log('this is before setting user to null');
      setUser(null);
      console.log('this is after setting user to null');
    } catch (error) {
      console.log('caught this error in the logout context function', error);
      if (error instanceof ApiError) {
        setErrorMessage(error.message);
      } else {
        setErrorMessage('An unexpected error occurred during logout.');
      }
    } finally {
      console.log('finally ran');
    }
  }

  const contextValue = {
    user,
    isAuthenticated,
    isLoading,
    errorMessage,
    login,
    logout,
  };

  return (
    <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>
  );
}
