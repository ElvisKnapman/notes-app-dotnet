import type { Note } from '../../../models/notes/Notes';

import './NoteItem.css';

interface NoteItemProps {
  note: Note;
}

const NoteItem = ({ note }: NoteItemProps) => {
  return (
    <div className='note-grid-item'>
      <div className='note-item-info-flex'>
        <h3 className='note-item-title'>{note.title}</h3>
        <div className='note-item-options'>
          <span>Edit</span>
          <span>Delete</span>
        </div>
      </div>
      <p>Last updated {note.updatedAt}</p>
    </div>
  );
};

export default NoteItem;
