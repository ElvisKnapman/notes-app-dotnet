import type { Note } from '../../models/notes/Notes';

interface NotesListProps {
  notes: Note[];
}

export default function NotesList({ notes }: NotesListProps) {
  return (
    <div>
      <p>
        {notes.length > 0
          ? `There are ${notes.length} notes.`
          : 'No notes to display. Add one!'}
      </p>
    </div>
  );
}
