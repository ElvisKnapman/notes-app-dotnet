export interface Note {
  id: string;
  title: string;
  content: string;
  createdAt: string;
  updatedAt: string;
  userId: string;
}

export type CreateNoteRequest = Pick<Note, 'title' | 'content'>;

export type UpdateNoteRequest = Partial<Pick<Note, 'title' | 'content'>>;
