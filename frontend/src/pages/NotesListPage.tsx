import { useEffect, useState } from 'react';
import NotesList from '../components/Notes/NoteList/NotesList';
import { getNotes, type GetNotesParams } from '../api/noteService';
import { ApiError } from '../errors/ApiError';
import type { Note } from '../models/notes/Notes';

export default function NotesListPage() {
  const [notes, setNotes] = useState<Note[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [query, setQuery] = useState<GetNotesParams>({
    pageSize: 10,
    pageNumber: 1,
    sortBy: 'createdAt',
    descending: true,
    searchTerm: '',
  });

  useEffect(() => {
    async function loadNotes() {
      setIsLoading(true);
      setErrorMessage(null);

      try {
        const data = await getNotes(query);
        setNotes(data.items);
      } catch (error) {
        if (error instanceof ApiError) {
          setErrorMessage(error.message);
        } else {
          setErrorMessage('Unknown error occurred fetching notes.');
        }
      } finally {
        setIsLoading(false);
      }
    }

    loadNotes();
  }, []);

  return (
    <>
      <h2>Notes Page</h2>
      {!isLoading && errorMessage !== null && <p>{errorMessage}</p>}
      {isLoading ? <p>Loading notes...</p> : <NotesList notes={notes} />}
    </>
  );
}
