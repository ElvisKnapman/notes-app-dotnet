import { BrowserRouter } from 'react-router';
import AppRoutes from './routes/AppRoutes';
import { AuthProvider } from './context/AuthContext';
// import NotesPage from './pages/NotesPage';

export default function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <AppRoutes />
      </AuthProvider>
    </BrowserRouter>
  );
}
