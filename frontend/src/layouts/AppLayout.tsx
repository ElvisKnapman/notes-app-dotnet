import { Outlet } from 'react-router';
import NavBar from '../components/NavBar/NavBar';

import './AppLayout.css';

export default function AppLayout() {
  return (
    <>
      <NavBar />
      <div className='content-wrapper'>
        <Outlet />
      </div>
    </>
  );
}
