using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("{id}")] 
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
        UserDto userDto = await _userService.AddUserAsync(userToCreate);

        return CreatedAtAction(nameof(GetById), new { Id = userDto.Id }, userDto);
    }
}
