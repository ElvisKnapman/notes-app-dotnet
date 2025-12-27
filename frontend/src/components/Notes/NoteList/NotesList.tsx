import type { Note } from '../../../models/notes/Notes';
import NoteItem from '../NoteItem/NoteIem';

import './NoteList.css';

interface NotesListProps {
  notes: Note[];
}

export default function NotesList({ notes }: NotesListProps) {
  return (
    <div>
      <p>
        {notes.length > 0
          ? `There ${notes.length === 1 ? 'is' : 'are'} ${notes.length} ${
              notes.length === 1 ? 'note' : 'notes'
            } to
        display.`
          : 'There are no notes to display.'}
      </p>
      <div className='note-grid'>
        {notes.map((note) => (
          <NoteItem key={note.id} note={note} />
        ))}
      </div>
    </div>
  );
}
