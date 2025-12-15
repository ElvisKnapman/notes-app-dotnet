import './App.css';
import NotesList from './components/NotesList';
import type { Note } from './models/notes/Note';

export default function App() {
  const notes: Note[] = [];
  return (
    <>
      <h1>Notes App</h1>
      <NotesList notes={notes} />
    </>
  );
}
