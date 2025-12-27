import { useContext } from 'react';
import { NoteContext } from './NoteContext';

export function useNotes() {
  const ctx = useContext(NoteContext);

  if (!ctx) {
    throw new Error('useNotes must be used within a NoteProvider');
  }

  return ctx;
}
