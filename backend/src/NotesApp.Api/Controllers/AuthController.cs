using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Constants;
using NotesApp.Api.DTOs.Requests.Users;
using NotesApp.Application.Common.Errors;
using NotesApp.Application.DTOs.Users;
using NotesApp.Application.Interfaces;

namespace NotesApp.Api.Controllers;

[ApiController]
[Route(RouteNames.Auth.Base)]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IJwtTokenService _tokenService;

    public AuthController(IAuthService authService, IJwtTokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    [HttpPost(RouteNames.Auth.Register)]
    public async Task<ActionResult> Register([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var createUserDto = new CreateUserDto(request.Username, request.Email, request.Password);

        var result = await _authService.RegisterUserAsync(createUserDto, cancellationToken);

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
    public async Task<ActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
    {
        var loginUserDto = new LoginUserDto(request.Email, request.Password);

        var result = await _authService.LoginAsync(loginUserDto, cancellationToken);

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
