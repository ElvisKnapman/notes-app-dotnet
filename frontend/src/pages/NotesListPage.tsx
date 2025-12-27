import { useEffect, useState } from 'react';
import NotesList from '../components/Notes/NoteList/NotesList';
import { useNotes } from '../context/useNotes';
import type { GetNotesParams } from '../api/noteService';

export default function NotesListPage() {
  const { notes, isLoading, errorMessage, loadNotes } = useNotes();
  const [query, setQuery] = useState<GetNotesParams>({
    pageSize: 10,
    pageNumber: 1,
    sortBy: 'createdAt',
    descending: true,
    searchTerm: '',
  });

  useEffect(() => {
    loadNotes();
  }, []);

  return (
    <>
      <h2>Notes Page</h2>
      {errorMessage && <p>{errorMessage}</p>}
      {isLoading ? <p>Loading notes...</p> : <NotesList notes={notes} />}
    </>
  );
}
