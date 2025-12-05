using Microsoft.AspNetCore.Mvc;
using NotesApp.Application.Common.Errors;
using NotesApp.Application.DTOs;
using NotesApp.Application.Interfaces;

namespace NotesApp.Api.Controllers;

[Route("api/users")]
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
        IEnumerable<UserDto> users = await _userService.GetAllUsersAsync();

        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    //[HttpPost]
    //public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto userToCreate)
    //{
    //    Result<UserDto> result = await _userService.AddUserAsync(userToCreate);

    //    if (!result.Success)
    //    {
    //        return BadRequest(result.ErrorMessage);
    //    }

    //    return CreatedAtAction(nameof(GetById), new { Id = result?.Value?.Id }, result?.Value);
    //}


    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto updateDto)
    {

        // Try to do the update
        var result = await _userService.UpdateUserAsync(id, updateDto);

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
