using Chat.Common.Requests;

namespace Chat.Web.Api.Interfaces;

public interface IAuthApiService
{
    Task RegisterAsync(UserRegisterRequest request);

    Task LoginAsync(UserLoginRequest request);

    Task<string> GetToken();

    Task LogoutAsync();
}