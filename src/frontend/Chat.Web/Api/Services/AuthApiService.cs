using System.Net.Http.Json;
using Blazored.LocalStorage;
using Chat.Common.Requests;
using Chat.Web.Api.Interfaces;

namespace Chat.Web.Api.Services;

public class AuthApiService : IAuthApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public AuthApiService(
        HttpClient httpClient,
        ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task LoginAsync(UserLoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/v1/auth/login", request);

        var token = await response.Content.ReadFromJsonAsync<string>();

        await _localStorage.SetItemAsync("authToken", token);
    }

    public async Task RegisterAsync(UserRegisterRequest request)
    {
        await _httpClient.PostAsJsonAsync("api/v1/auth/register", request);
    }

    public async Task<string> GetToken()
    {
        return await _localStorage.GetItemAsync<string>("authToken");
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
    }
}