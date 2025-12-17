import { useState } from 'react';

import './AppLayout.css';

interface AppLayoutProps {
  children: React.ReactNode;
}

export default function AppLayout({ children }: AppLayoutProps) {
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  function handleLoggedInStatus() {
    setIsLoggedIn((prev) => !prev);
  }
  return (
    <>
      <header>
        <h1>Notes App</h1>
        <nav>
          <ul className='nav-links'>
            {!isLoggedIn ? (
              <>
                <li onClick={handleLoggedInStatus} className='nav-link'>
                  Login
                </li>
                <li className='nav-link'>Signup</li>
              </>
            ) : (
              <>
                <li className='nav-link'>Notes</li>
                <li className='nav-link'>Profile</li>
                <li onClick={handleLoggedInStatus} className='nav-link'>
                  Logout
                </li>
              </>
            )}
          </ul>
        </nav>
      </header>
      <div className='content-wrapper' style={{ border: '1px dashed black' }}>
        {children}
      </div>
    </>
  );
}
