import { useState } from 'react';
import type { AuthUser } from '../models/users/Users';
import { getMe, login } from '../api/authService';
import { ApiError } from '../errors/ApiError';

export default function LoginPage() {
  const [user, setUser] = useState<AuthUser | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const isAuthenticated = user !== null;
  console.log('is authenticated?', isAuthenticated);
  console.log('user in state', user);

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setError(null);
    setIsLoading(true);

    try {
      await login(email, password);
      const result = await getMe();
      setUser(result.data);
    } catch (err) {
      if (err instanceof ApiError) {
        setError(err.message);
      }
    } finally {
      setIsLoading(false);
    }
  }
  return (
    <div>
      <h2>Login Page</h2>
      <form onSubmit={handleSubmit}>
        <div className='form-group'>
          <label htmlFor='email'>Email</label>
          <input
            type='text'
            name='email'
            id='email'
            onChange={(e) => setEmail(e.target.value)}
            value={email}
          />
        </div>
        <div className='form-group'>
          <label htmlFor='password'>Password</label>
          <input
            type='text'
            name='password'
            id='password'
            onChange={(e) => setPassword(e.target.value)}
            value={password}
          />
        </div>
        <button type='submit' disabled={isLoading}>
          {isLoading ? 'Logging in...' : 'Login'}
        </button>
        {error !== null && <p style={{ color: 'red' }}>{error}</p>}
      </form>
    </div>
  );
}
