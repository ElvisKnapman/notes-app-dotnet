import type { Note } from '../models/notes/Note';

interface NotesListProps {
  notes: Note[];
}

export default function NotesList({ notes }: NotesListProps) {
  return (
    <div>
      <h2>Notes List</h2>
      <div>
        {notes.length > 0 ? `There are ${notes.length} notes.` : 'No notes.'}
      </div>
    </div>
  );
}
