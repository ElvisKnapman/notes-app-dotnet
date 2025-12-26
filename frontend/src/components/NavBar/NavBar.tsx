import { Link } from 'react-router';
import { useAuth } from '../../context/useAuth';

export default function NavBar() {
  const { isAuthenticated, logout } = useAuth();
  return (
    <header>
      <h1>Notes App</h1>
      <nav>
        <ul className='nav-links'>
          {!isAuthenticated ? (
            <>
              <li className='nav-link btn btn-accent'>
                <Link to='/login'>Login</Link>
              </li>
              <li className='nav-link btn btn-accent'>
                <Link to='/signup'>Signup</Link>
              </li>
            </>
          ) : (
            <>
              <li className='nav-link btn btn-accent'>
                <Link to='/notes'>Notes</Link>
              </li>
              <li className='nav-link btn btn-accent'>
                <Link to='/profile'>Profile</Link>
              </li>
              <li className='nav-link btn btn-accent' onClick={logout}>
                Logout
              </li>
            </>
          )}
        </ul>
      </nav>
    </header>
  );
}
