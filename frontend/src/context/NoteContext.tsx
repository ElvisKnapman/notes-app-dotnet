import { createContext, useState } from 'react';
import type {
  CreateNoteRequest,
  Note,
  UpdateNoteRequest,
} from '../models/notes/Notes';
import { getNotes, type GetNotesParams } from '../api/noteService';
import { ApiError } from '../errors/ApiError';

interface NoteContextValue {
  notes: Note[];
  isLoading: boolean;
  errorMessage: string | null;
  loadNotes: (params: GetNotesParams) => Promise<void>;
  createNote: (noteToCreate: CreateNoteRequest) => Promise<void>;
  updateNote: (id: string, noteToUpdate: UpdateNoteRequest) => Promise<void>;
  deleteNote: (id: string) => Promise<void>;
}

interface NoteContextProviderProps {
  children: React.ReactNode;
}

export const NoteContext = createContext<NoteContextValue | undefined>(
  undefined
);

export function NoteProvider({ children }: NoteContextProviderProps) {
  const [notes, setNotes] = useState<Note[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  async function loadNotes(params: GetNotesParams): Promise<void> {
    setErrorMessage(null);
    setIsLoading(true);

    try {
      const result = await getNotes(params);
      setNotes(result.data.items);
    } catch (err) {
      if (err instanceof ApiError) {
        setErrorMessage(err.message);
      } else {
        setErrorMessage('Error has occurred.');
      }
    } finally {
      setIsLoading(false);
    }
  }

  async function createNote(noteToCreate: CreateNoteRequest): Promise<void> {}

  async function updateNote(
    id: string,
    noteToUpdate: UpdateNoteRequest
  ): Promise<void> {}

  async function deleteNote(id: string): Promise<void> {}

  const contextValue: NoteContextValue = {
    notes,
    isLoading,
    errorMessage,
    loadNotes,
    createNote,
    updateNote,
    deleteNote,
  };

  return (
    <NoteContext.Provider value={contextValue}>{children}</NoteContext.Provider>
  );
}
