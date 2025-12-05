using Microsoft.AspNetCore.Mvc;
using NotesApp.Application.DTOs;

namespace NotesApp.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] CreateUserDto dto, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserDto dto)
    {
        return Ok();
    }
}
