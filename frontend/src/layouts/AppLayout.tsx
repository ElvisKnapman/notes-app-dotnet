import { Outlet } from 'react-router';
import NavBar from '../components/NavBar';

import './AppLayout.css';

export default function AppLayout() {
  return (
    <>
      <NavBar />
      <div className='main-content-wrapper'>
        <Outlet />
      </div>
    </>
  );
}
