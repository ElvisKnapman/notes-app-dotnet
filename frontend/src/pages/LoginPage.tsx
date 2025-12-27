import { useState } from 'react';
import { useAuth } from '../context/useAuth';
import { useNavigate } from 'react-router';

export default function LoginPage() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const navigate = useNavigate();

  const { user, login, isAuthenticated, isLoading, errorMessage, logout } =
    useAuth();

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    await login(email, password);
    navigate('/notes');
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
        <button type='button' onClick={logout}>
          Call Logout
        </button>
        {errorMessage !== null && (
          <p style={{ color: 'red' }}>{errorMessage}</p>
        )}
      </form>
    </div>
  );
}
