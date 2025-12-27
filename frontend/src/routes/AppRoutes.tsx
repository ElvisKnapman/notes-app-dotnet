import { Route, Routes } from 'react-router';
import AppLayout from '../layouts/AppLayout';
import LoginPage from '../pages/LoginPage';
import NotesListPage from '../pages/NotesListPage';
import HomePage from '../pages/HomePage';
import ProtectedRoute from './ProtectedRoute';

export default function AppRoutes() {
  return (
    <Routes>
      <Route element={<AppLayout />}>
        <Route path='/' element={<HomePage />} />
        <Route path='/login' element={<LoginPage />} />

        <Route element={<ProtectedRoute />}>
          <Route path='/notes' element={<NotesListPage />} />
        </Route>
      </Route>
    </Routes>
  );
}
