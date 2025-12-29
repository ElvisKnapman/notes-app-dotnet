import { API_BASE_URL } from '../config/apiConfig';
import type {
  CreateNoteRequest,
  Note,
  UpdateNoteRequest,
} from '../models/notes/Notes';
import { http } from './http';

export interface GetNotesParams {
  pageNumber?: number;
  pageSize?: number;
  searchTerm?: string;
  sortBy?: string;
  descending?: boolean;
}

interface GetNotesResponse {
  data: {
    totalCount: number;
    pageNumber: number;
    pageSize: number;
    items: Note[];
  };
  success: boolean;
}

export interface PagedResponse {
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  items: Note[];
}

interface SingleNoteResponse {
  data: Note;
  success: boolean;
}

/* 
  data is flattened in the following methods (returning data property) to not have to repeatedly
  drill down into JSON response
*/

export async function getNotes(
  params: GetNotesParams = {}
): Promise<PagedResponse> {
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

  const result = await http<GetNotesResponse>(
    `${API_BASE_URL}/notes?${query.toString()}`
  );

  return result.data;
}

export async function getNoteById(id: string): Promise<Note> {
  const result = await http<SingleNoteResponse>(`${API_BASE_URL}/notes/${id}`);

  return result.data;
}

export async function createNote(newNote: CreateNoteRequest): Promise<Note> {
  const result = await http<SingleNoteResponse>(`${API_BASE_URL}/notes`, {
    method: 'POST',
    body: JSON.stringify(newNote),
  });

  return result.data;
}

export async function updateNote(
  id: string,
  updatedNote: UpdateNoteRequest
): Promise<Note> {
  const result = await http<SingleNoteResponse>(`${API_BASE_URL}/notes/${id}`, {
    method: 'PUT',
    body: JSON.stringify(updatedNote),
  });

  return result.data;
}

export async function deleteNote(id: string): Promise<void> {
  return http<void>(`${API_BASE_URL}/notes/${id}`);
}
