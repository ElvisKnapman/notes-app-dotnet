using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Constants;
using NotesApp.Api.DTOs.Requests.Notes;
using NotesApp.Api.DTOs.Responses;
using NotesApp.Api.Extensions;
using NotesApp.Application.Common.Errors;
using NotesApp.Application.DTOs.Notes;
using NotesApp.Application.Interfaces;

namespace NotesApp.Api.Controllers;

[ApiController]
[Route(RouteNames.Notes.Base)]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;
    private readonly ILogger<NotesController> _logger;

    public NotesController(INoteService noteService, ILogger<NotesController> logger)
    {
        _noteService = noteService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var users = await _noteService.GetAllAsync(cancellationToken);

        return Ok(new SuccessResponse<IEnumerable<NoteDto>>(users.Value));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateNote(
        [FromBody] CreateNoteRequest request,
        CancellationToken cancellationToken
    )
    {
        var something = HttpContext;
        // Call extension method to get GUID ID from JWT claims
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
                    BadRequest(new ErrorResponse(result.ErrorCode, result.ErrorMessage)),
                _ => StatusCode(500, new ErrorResponse(result.ErrorCode, result.ErrorMessage))
            };
        }

        var noteDto = result.Value;

        return Ok(noteDto);
    }
}
