using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Constants;
using NotesApp.Api.DTOs;
using NotesApp.Api.DTOs.Requests.Users;
using NotesApp.Api.DTOs.Responses;
using NotesApp.Application.Common.Errors;
using NotesApp.Application.DTOs.Users;
using NotesApp.Application.Interfaces;

namespace NotesApp.Api.Controllers;

[ApiController]
[Route(RouteNames.Auth.Base)]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly ITokenStore _tokenStore;

    public AuthController(IAuthService authService, ITokenService tokenService, ITokenStore tokenStore)
    {
        _authService = authService;
        _tokenService = tokenService;
        _tokenStore = tokenStore;
    }

    [HttpPost(RouteNames.Auth.Register)]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var createUserDto = new CreateUserDto(request.Username, request.Email, request.Password);

        var result = await _authService.RegisterUserAsync(createUserDto, cancellationToken);

        if (!result.Success)
        {
            return result.ErrorCode switch
            {
                ErrorCodes.EmailExists => BadRequest(new ErrorResponse(result.ErrorCode, result.ErrorMessage)),
                ErrorCodes.UsernameExists => BadRequest(new ErrorResponse(result.ErrorCode, result.ErrorMessage)),
                ErrorCodes.CreationFailed => StatusCode(500, new ErrorResponse(result.ErrorCode, result.ErrorMessage)),
                _ => BadRequest()
            };
        }

        var userDto = result.Value;

        return CreatedAtRoute(
            nameof(UsersController.GetById), new { Id = userDto.Id }, userDto
            );
    }

    [HttpPost(RouteNames.Auth.Login)]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken = default)
    {
        var loginUserDto = new LoginUserDto(request.Email, request.Password);

        var context = HttpContext;

        var result = await _authService.LoginAsync(loginUserDto, cancellationToken);

        if (!result.Success)
        {
            return result.ErrorCode switch
            {
                ErrorCodes.UserNotFound => NotFound(new ErrorResponse(result.ErrorCode, result.ErrorMessage)),
                ErrorCodes.InvalidCredentials => Unauthorized(new ErrorResponse(result.ErrorCode, result.ErrorMessage)),
                _ => StatusCode(500, new ErrorResponse(result.ErrorCode, result.ErrorMessage))
            };
        }

        var userDto = result.Value;

        var token = _tokenService.GenerateToken(userDto);

        // Add token to cookie on response
        _tokenStore.Set(token);


        return Ok(new SuccessResponse<JwtTokenDto>(new JwtTokenDto(token)));
    }
}
