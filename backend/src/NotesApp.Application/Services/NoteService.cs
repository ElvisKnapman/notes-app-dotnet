using Microsoft.Extensions.Logging;
using NotesApp.Application.Common;
using NotesApp.Application.Common.Errors;
using NotesApp.Application.DTOs.Notes;
using NotesApp.Application.Interfaces;
using NotesApp.Application.Mappers;

namespace NotesApp.Application.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepo;
    private readonly IUnitOfWork _uow;
    private ILogger<NoteService> _logger;

    public NoteService(INoteRepository noteRepo, IUnitOfWork uow, ILogger<NoteService> logger)
    {
        _noteRepo = noteRepo;
        _uow = uow;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<NoteDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var notes = await _noteRepo.GetAllAsync(cancellationToken);

        return Result<IEnumerable<NoteDto>>.Ok(notes.Select(n => n.ToNoteDto()));
    }

    public async Task<Result<NoteDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Attempting to retrive note with ID: {ID}", id);

        var note = await _noteRepo.GetByIdNoTrackingAsync(id, cancellationToken);

        if (note is null)
        {
            _logger.LogWarning("No note was found with ID: {ID}", id);

            return Result<NoteDto>.Fail(ErrorCodes.NoteNotFound, ErrorMessages.NoteNotFoundWithID);
        }

        _logger.LogInformation("Successfully retrieved note with ID: {ID}", id);

        return Result<NoteDto>.Ok(note.ToNoteDto());
    }

    public async Task<Result<NoteDto>> CreateAsync(
        CreateNoteDto createNoteDto,
        CancellationToken cancellationToken = default
    )
    {
        var noteEntity = createNoteDto.ToNoteEntity();

        _logger.LogInformation("Attempting to create note for user with ID: {ID}",
            noteEntity.UserId);

        // Add note to change tracker
        _noteRepo.Add(noteEntity);

        var changes = await _uow.SaveChangesAsync(cancellationToken);

        if (changes < 1)
        {
            _logger.LogWarning("Note for user with ID: {ID} failed to save successfully.",
                noteEntity.UserId);

            return Result<NoteDto>.Fail(ErrorCodes.CreationFailed, ErrorMessages.CreationFailed);
        }

        _logger.LogInformation("Note saved successfully for user with ID: {ID}", noteEntity.UserId);

        return Result<NoteDto>.Ok(noteEntity.ToNoteDto());
    }

    public async Task<Result<NoteDto>> UpdateAsync(
        UpdateNoteDto updateNoteDto,
        CancellationToken cancellationToken = default
    )
    {
        _logger.LogInformation("Attempting to update note with ID: {ID}", updateNoteDto.Id);

        var noteEntity = await _noteRepo.GetByIdAsync(updateNoteDto.Id, cancellationToken);

        if (noteEntity is null)
        {
            _logger.LogWarning("No  note was found with ID: {ID}", updateNoteDto.Id);

            return Result<NoteDto>.Fail(ErrorCodes.NoteNotFound, ErrorMessages.NoteNotFoundWithID);
        }

        // Map updated fields
        noteEntity.UpdateNoteEntity(updateNoteDto);

        // No need to add to change tracker as entity is already being tracked
        var changes = await _uow.SaveChangesAsync(cancellationToken);

        if (changes < 1)
        {
            _logger.LogWarning("Note with ID: {ID} failed to update successfully.", noteEntity.Id);

            return Result<NoteDto>.Fail(ErrorCodes.UpdateFailed, ErrorMessages.UpdateFailed);
        }

        _logger.LogInformation("Note with ID: {ID} updated successfully.", noteEntity.Id);

        return Result<NoteDto>.Ok(noteEntity.ToNoteDto());
    }
}
