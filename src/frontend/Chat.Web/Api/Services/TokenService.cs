using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Chat.Web.Api.Interfaces;

namespace Chat.Web.Api.Services;

public class TokenService : ITokenService
{
    private readonly ILocalStorageService _localStorage;

    public TokenService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<string?> GetUserNameAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");

        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userNameClaim = jwtToken.Claims.FirstOrDefault(c =>
            c.Type == ClaimTypes.Name)?.Value;

        return userNameClaim;
    }

    public async Task<int?> GetUserIdAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");

        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userIdClaim = int.Parse(jwtToken.Claims.FirstOrDefault(c =>
            c.Type == ClaimTypes.NameIdentifier)?.Value);

        return userIdClaim;
    }
}