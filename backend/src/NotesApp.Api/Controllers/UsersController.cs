using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Constants;
using NotesApp.Api.DTOs.Requests.Users;
using NotesApp.Api.DTOs.Responses;
using NotesApp.Application.Common.Errors;
using NotesApp.Application.DTOs.Users;
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
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(CancellationToken cancellationToken = default)
    {
        var result = await _userService.GetAllAsync(cancellationToken);

        return Ok(new SuccessResponse<IEnumerable<UserDto>>(result.Value));
    }

    [HttpGet(RouteNames.Users.GetById, Name = nameof(GetById))]
    public async Task<ActionResult<UserDto>> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _userService.GetByIdAsync(id, cancellationToken);

        if (!result.Success)
        {
            return NotFound(new ErrorResponse(result.ErrorCode, result.ErrorMessage));
        }

        return Ok(new SuccessResponse<UserDto>(result.Value));
    }

    [HttpPut(RouteNames.Users.Update)]
    public async Task<ActionResult> UpdateUser(
        Guid id,
        [FromBody] UpdateUserRequest updateRequest,
        CancellationToken cancellationToken)
    {

        var dto = new UpdateUserDto(updateRequest.Username, updateRequest.Email);

        // Try the update
        var result = await _userService.UpdateAsync(id, dto, cancellationToken);

        // Check the result
        if (!result.Success)
        {
            return result.ErrorCode switch
            {
                ErrorCodes.UserNotFound => NotFound(new ErrorResponse(result.ErrorCode, result.ErrorMessage)),
                ErrorCodes.EmailExists or
                ErrorCodes.UsernameExists or
                ErrorCodes.UpdateFailed => BadRequest(new ErrorResponse(result.ErrorCode, result.ErrorMessage)),
                _ => BadRequest(new ErrorResponse(result.ErrorCode, result.ErrorMessage))
            };
        }

        return Ok(new SuccessResponse<UserDto>(result.Value));
    }
}
