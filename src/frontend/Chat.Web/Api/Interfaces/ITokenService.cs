namespace Chat.Web.Api.Interfaces;

public interface ITokenService
{
    Task<string?> GetUserNameAsync();

    Task<int?> GetUserIdAsync();
}