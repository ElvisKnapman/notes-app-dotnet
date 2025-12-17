import AppLayout from './layouts/AppLayout';
import LoginPage from './pages/LoginPage';
// import NotesPage from './pages/NotesPage';

export default function App() {
  return (
    <AppLayout>
      {/* <NotesPage /> */}
      <LoginPage />
    </AppLayout>
  );
}
