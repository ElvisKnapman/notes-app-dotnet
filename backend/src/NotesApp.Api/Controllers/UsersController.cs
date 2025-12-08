using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Constants;
using NotesApp.Application.Common.Errors;
using NotesApp.Application.DTOs;
using NotesApp.Application.Interfaces;

namespace NotesApp.Api.Controllers;

[Route(RouteNames.Users.Base)]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var result = await _userService.GetAllAsync();

        return Ok(result.Value);
    }

    [HttpGet(RouteNames.Users.GetById, Name = nameof(GetById))]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        var result = await _userService.GetByIdAsync(id);

        if (!result.Success)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto updateDto)
    {

        // Try to do the update
        var result = await _userService.UpdateAsync(updateDto);

        // Check the result
        if (!result.Success)
        {
            var errorResponse = new
            {
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage
            };

            return result.ErrorCode switch
            {
                ErrorCodes.UserNotFound => NotFound(errorResponse),
                ErrorCodes.EmailExists => BadRequest(errorResponse),
                ErrorCodes.UsernameExists => BadRequest(errorResponse),
                ErrorCodes.UpdateFailed => BadRequest(errorResponse),
                _ => BadRequest(errorResponse)
            };
        }

        return Ok(result.Value);
    }
}
