export default function NavBar() {
  const isLoggedIn = false;
  function handleLoggedInStatus() {}
  return (
    <header>
      <h1>Notes App</h1>
      <nav>
        <ul className='nav-links'>
          {!isLoggedIn ? (
            <>
              <li
                onClick={handleLoggedInStatus}
                className='nav-link btn btn-accent'
              >
                Login
              </li>
              <li className='nav-link btn btn-accent'>Signup</li>
            </>
          ) : (
            <>
              <li className='nav-link btn btn-accent'>Notes</li>
              <li className='nav-link btn btn-accent'>Profile</li>
              <li
                onClick={handleLoggedInStatus}
                className='nav-link btn btn-accent'
              >
                Logout
              </li>
            </>
          )}
        </ul>
      </nav>
    </header>
  );
}
