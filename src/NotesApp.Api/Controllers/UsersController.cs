using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Test.Api.Controllers;

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
        IEnumerable<UserDto> users = _users.Select(u => u.ToUserDto());

        return Ok(users);
    }

    [HttpGet("{id}")] 
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        var user = _users.Find(u => u.Id == id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto userDto)
    {
        User user = userDto.ToUserEntity();
        _users.Add(user);

        return CreatedAtAction(nameof(GetById), new { Id = user.Id }, user.ToUserDto());
    }
}
