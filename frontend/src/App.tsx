import { BrowserRouter } from 'react-router';
import AppRoutes from './routes/AppRoutes';
import { AuthProvider } from './context/AuthContext';
import { NoteProvider } from './context/NoteContext';
// import NotesPage from './pages/NotesPage';

export default function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <NoteProvider>
          <AppRoutes />
        </NoteProvider>
      </AuthProvider>
    </BrowserRouter>
  );
}
