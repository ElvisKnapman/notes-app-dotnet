import { Navigate, Outlet } from 'react-router';
import { useAuth } from '../context/useAuth';

export default function ProtectedRoute() {
  const { isAuthenticated } = useAuth();
  console.log('isAutehnticated in ProtectedRoute:', isAuthenticated);

  console.log('ProtectedRoute component rendered');
  if (!isAuthenticated) return <Navigate to='/login' replace />;

  return <Outlet />;
}
