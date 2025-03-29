using Application.Core;
using Application.DTOs.Auth;
using Application.Services;
using Application.Utilities;
using Application.Utulity;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Persistence.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<string>> RegisterAsync(RegisterDto dto)
    {
        var user = new AppUser
        {
            FullName = dto.FullName,
            Email = dto.Email,
            UserName = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result<string>.Failure(MessageGenerator.RegistrationFailed(errors), 400);
        }

        await _userManager.AddToRoleAsync(user, SD.Role_User);

        var token = await _tokenService.CreateToken(user);
        return Result<string>.Success(token, MessageGenerator.RegistrationSuccess());
    }

    public async Task<Result<string>> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            return Result<string>.Failure(MessageGenerator.InvalidCredentials(), 401);

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

        if (!result.Succeeded)
            return Result<string>.Failure(MessageGenerator.InvalidCredentials(), 401);

        var token = await _tokenService.CreateToken(user);
        return Result<string>.Success(token, MessageGenerator.LoginSuccess());
    }

    public async Task<Result<bool>> LogoutAsync()
    {
        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrWhiteSpace(token))
            return Result<bool>.Failure("Token is required", 400);

        await _tokenService.RevokeTokenAsync(token);
        return Result<bool>.Success(true, MessageGenerator.LogoutSuccess());
    }

    public async Task<Result<UserDto>> GetCurrentUserAsync()
    {
        var email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(email))
            return Result<UserDto>.Failure(MessageGenerator.Unauthorized(), 401);

        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            return Result<UserDto>.Failure(MessageGenerator.NotFound("User"), 404);

        var userDto = new UserDto
        {
            FullName = user.FullName,
            Email = user.Email!
        };

        return Result<UserDto>.Success(userDto);
    }
}
