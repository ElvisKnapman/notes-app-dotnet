using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Constants;
using NotesApp.Api.DTOs.Requests.Notes;
using NotesApp.Api.DTOs.Responses;
using NotesApp.Api.Extensions;
using NotesApp.Application.Common.Constants;
using NotesApp.Application.Common.Errors;
using NotesApp.Application.DTOs.Common;
using NotesApp.Application.DTOs.Notes;
using NotesApp.Application.Interfaces;

namespace NotesApp.Api.Controllers;

[ApiController]
[Route(RouteNames.Notes.Base)]
[Authorize]
public class NotesController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly INoteService _noteService;
    private readonly ILogger<NotesController> _logger;

    public NotesController(
        INoteService noteService,
        ILogger<NotesController> logger,
        IAuthorizationService authorizationService
    )
    {
        _authorizationService = authorizationService;
        _noteService = noteService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyNotes(
        [FromQuery] NoteQueryRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var userId = User.GetUserId();

        var noteQueryDto = new NoteQueryDto(
            request.PageSize,
            request.PageNumber,
            request.SortBy,
            request.Descending,
            request.Search
        );

        var result = await _noteService.GetNotesForUserAsync(userId, noteQueryDto, cancellationToken);

        return Ok(new SuccessResponse<PagedResult<NoteDto>>(result.Value));
    }

    [HttpPost]
    public async Task<IActionResult> CreateNote(
        [FromBody] CreateNoteRequest request,
        CancellationToken cancellationToken
    )
    {
        // Call extension method to get GUID ID from authenticated JWT claims
        var userId = User.GetUserId();

        var createNoteDto = new CreateNoteDto(userId, request.Title, request.Content);

        var result = await _noteService.CreateAsync(createNoteDto, cancellationToken);

        if (!result.Success)
        {
            return result.ErrorCode switch
            {
                ErrorCodes.CreationFailed or
                _ => StatusCode(500, new ErrorResponse(result.ErrorCode, result.ErrorMessage))
            };
        }

        var createdNote = result.Value;

        return CreatedAtAction(nameof(GetById), new { Id = createdNote.Id }, createdNote);
    }

    [HttpGet(RouteNames.Notes.GetById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _noteService.GetByIdAsync(id);

        if (!result.Success)
        {
            return result.ErrorCode switch
            {
                ErrorCodes.NoteNotFound =>
                    NotFound(new ErrorResponse(result.ErrorCode, result.ErrorMessage)),
                _ => StatusCode(500, new ErrorResponse(result.ErrorCode, result.ErrorMessage))
            };
        }

        var noteDto = result.Value;

        return Ok(noteDto);
    }

    [HttpPut(RouteNames.Notes.Update)]
    public async Task<IActionResult> UpdateNote(
        Guid id,
        [FromBody] UpdateNoteRequest request,
        CancellationToken cancellationToken
    )
    {
        var note = await _noteService.GetEntityByIdAsync(id, cancellationToken);

        if (note is null)
        {
            return NotFound(new ErrorResponse(ErrorCodes.NoteNotFound, ErrorMessages.NoteNotFoundWithID));
        }

        // Authorization checking if the user is the owner of the note
        var authorized = await _authorizationService.AuthorizeAsync(
            User,
            note,
            AuthorizationPolicyNames.MustBeNoteOwner
        );

        if (!authorized.Succeeded) return Forbid();

        var updateDto = new UpdateNoteDto(id, request.Title, request.Content);

        var result = await _noteService.UpdateAsync(note, updateDto, cancellationToken);

        if (!result.Success)
        {
            return result.ErrorCode switch
            {
                ErrorCodes.NoteNotFound => NotFound(new ErrorResponse(result.ErrorCode, result.ErrorMessage)),
                ErrorCodes.UpdateFailed
                or _ => StatusCode(500, new ErrorResponse(result.ErrorCode, result.ErrorMessage)),
            };
        }

        return NoContent();
    }
}
