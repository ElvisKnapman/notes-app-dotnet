using Microsoft.AspNetCore.Mvc;
using NotesApp.Application.DTOs;
using NotesApp.Application.Interfaces;
using NotesApp.Domain.Common;

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

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto userToCreate)
    {
        Result<UserDto> result = await _userService.AddUserAsync(userToCreate);

        if (!result.Success)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetById), new { Id = result?.Value?.Id }, result?.Value);
    }

    [HttpPost("hash-password")]
    public async Task<ActionResult<string>> HashPassword([FromBody] string password)
    { 
        return Ok(await _userService.HashPassword(password));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto updateDto)
    {
        // Try to do the update
        
        
        // Check the result


        // Return the responses
    }
}
