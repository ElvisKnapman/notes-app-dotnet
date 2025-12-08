using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Constants;
using NotesApp.Application.Common.Errors;
using NotesApp.Application.DTOs;
using NotesApp.Application.Interfaces;

namespace NotesApp.Api.Controllers;

[ApiController]
[Route(RouteNames.Auth.Base)]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    [HttpPost(RouteNames.Auth.Register)]
    public async Task<ActionResult> Register([FromBody] CreateUserDto dto, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterUserAsync(dto, cancellationToken);

        if (!result.Success)
        {
            return result.ErrorCode switch
            {
                ErrorCodes.EmailExists => BadRequest(ErrorMessages.EmailExists),
                ErrorCodes.UsernameExists => BadRequest(ErrorMessages.UsernameExists),
                ErrorCodes.CreationFailed => StatusCode(500, ErrorMessages.CreationFailed),
                _ => BadRequest()
            };
        }

        var userDto = result.Value;

        return CreatedAtRoute(
            nameof(UsersController.GetById), new { Id = userDto.Id }, userDto
            );
    }

    [HttpPost(RouteNames.Auth.Login)]
    public async Task<ActionResult> Login([FromBody] LoginUserDto dto, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(dto, cancellationToken);

        if (!result.Success)
        {
            return result.ErrorCode switch
            {
                ErrorCodes.UserNotFound => BadRequest(ErrorMessages.UserNotFound),
                ErrorCodes.InvalidCredentials => Unauthorized(ErrorMessages.InvalidCredentials),
                _ => StatusCode(500, "An unexpected error occurred.")
            };
        }

        var userDto = result.Value;

        var token = _tokenService.GenerateToken(userDto);

        return Ok(new { token });
    }
}
