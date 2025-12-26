import { Navigate, Outlet } from 'react-router';
import { useAuth } from '../context/useAuth';
import Spinner from '../components/common/Spinner';

export default function ProtectedRoute() {
  const { isAuthenticated, authChecked } = useAuth();
  // const authChecked = false;
  console.log('isAuthenticated in ProtectedRoute:', isAuthenticated);
  console.log('authChecked in ProtectedRoute:', authChecked);

  // If auth status is still being checked, show a loading spinner
  if (!authChecked) return <Spinner />;

  if (!isAuthenticated) return <Navigate to='/login' replace />;

  return <Outlet />;
}
