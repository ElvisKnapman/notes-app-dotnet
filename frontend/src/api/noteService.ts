import { API_BASE_URL } from '../config/apiConfig';
import type {
  CreateNoteRequest,
  Note,
  UpdateNoteRequest,
} from '../models/notes/Notes';
import { http } from './http';

export interface LoadNotesParams {
  pageNumber?: number;
  pageSize?: number;
  searchTerm?: string;
  sortBy?: string;
  descending?: boolean;
}

interface LoadNotesResponse {
  data: {
    totalCount: number;
    pageNumber: number;
    pageSize: number;
    items: Note[];
  };
  success: boolean;
}

export interface SingleNoteResponse {
  data: Note;
  success: boolean;
}

export async function loadNotes(
  params: LoadNotesParams = {}
): Promise<LoadNotesResponse> {
  const {
    pageNumber = 1,
    pageSize = 10,
    searchTerm = '',
    sortBy = 'createdAt',
    descending = false,
  } = params;

  const query = new URLSearchParams({
    pageNumber: pageNumber.toString(),
    pageSize: pageSize.toString(),
    searchTerm,
    sortBy,
    descending: descending.toString(),
  });

  return http<LoadNotesResponse>(`${API_BASE_URL}/notes?${query.toString()}`);
}

export async function createNote(
  newNote: CreateNoteRequest
): Promise<SingleNoteResponse> {
  return http<SingleNoteResponse>(`${API_BASE_URL}/notes`, {
    method: 'POST',
    body: JSON.stringify(newNote),
  });
}

export async function updateNote(
  id: string,
  updatedNote: UpdateNoteRequest
): Promise<SingleNoteResponse> {
  return http<SingleNoteResponse>(`${API_BASE_URL}/notes/${id}`, {
    method: 'PUT',
    body: JSON.stringify(updatedNote),
  });
}

export async function deleteAsync(id: string): Promise<void> {
  return http<void>(`${API_BASE_URL}/notes/${id}`);
}
