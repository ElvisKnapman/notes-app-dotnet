import { useEffect, useState } from 'react';
import NotesList from '../components/NotesList';
import type { Note } from '../models/notes/Note';
import { ApiError } from '../errors/ApiError';

export default function NotesPage() {
  const [notes, setNotes] = useState<Note[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    loadNotes();
  }, []);

  async function loadNotes(): Promise<void> {
    try {
      setIsLoading(true);
      const token =
        'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiZjExNDA5MDQtN2VkMS00MTRjLWE5ZGUtMTI1Y2M2YjJkZWVmIiwiZW1haWxfYWRkcmVzcyI6ImtuYXBtYW4ubWF0dEBnbWFpbC5jb20iLCJ1c2VybmFtZSI6Im1hdHRrbmFwbWFuIiwibmJmIjoxNzY1ODMwMzY1LCJleHAiOjE3NjU5MDIzNjUsImlhdCI6MTc2NTgzMDM2NSwiaXNzIjoiTm90ZXNBcHAiLCJhdWQiOiJOb3Rlc0FwcFVzZXJzIn0.nedtYEml_1kcrGNBAihe81OAh7-6e4FMgwryPh7YniM';
      const response = await fetch('https://localhost:7048/api/notes', {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (!response.ok) {
        let message = '';

        switch (response.status) {
          case 401:
            message = 'Unauthorized';
            break;
          case 403:
            message = 'Forbidden';
            break;
          case 500:
            message = 'Server error';
            break;
        }

        throw new ApiError(response.status, message);
      }
      const data: {
        data: {
          count: number;
          pageSize: number;
          pageCount: number;
          items: Note[];
          success: boolean;
        };
      } = await response.json();
      setNotes(data.data.items);
    } catch (error) {
      if (error instanceof ApiError) {
        console.log('the status code was', error.statusCode);
        console.log('the message was', error.message);
      }
    } finally {
      setIsLoading(false);
    }
  }

  return (
    <>
      <h2>Notes Page</h2>
      {isLoading ? <p>Loading notes...</p> : <NotesList notes={notes} />}
    </>
  );
}
