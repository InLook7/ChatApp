using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using FluentResults;
using Chat.Application.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Chat.Application.Dtos;

namespace Chat.Infrastructure.Auth;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUnitOfWork unitOfWork,
        IPasswordHasher<User> passwordHasher,
        IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    public async Task<Result<string>> LoginAsync(UserDto userDto)
    {
        var user = await _unitOfWork.UserRepository.GetByUserNameAsync(userDto.UserName);
        if (user == null)
        {
            return Result.Fail("User with the provided username does not exist.");
        }

        var hashResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userDto.Password);
        if (hashResult != PasswordVerificationResult.Success)
        {
            return Result.Fail("Incorrect password.");
        }

        var token = GenerateToken(user);

        return Result.Ok(token);
    }

    public async Task<Result> RegisterAsync(UserDto userDto)
    {
        var userCheck = await _unitOfWork.UserRepository.GetByUserNameAsync(userDto.UserName);
        if (userCheck != null)
        {
            return Result.Fail("User with the provided username already exists.");
        }

        var user = new User
        {
            UserName = userDto.UserName
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, userDto.Password);

        await _unitOfWork.UserRepository.CreateAsync(user);
        await _unitOfWork.SaveAsync();

        return Result.Ok();
    }

    private string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}