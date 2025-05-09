using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Blazored.LocalStorage;
using Chat.Web;
using Chat.Web.Api.Interfaces;
using Chat.Web.Api.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(serviceProdiver => new HttpClient
{
    BaseAddress = new Uri("{CHAT_APP_BACKEND}")
});

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IRoomApiService, RoomApiService>();
builder.Services.AddScoped<IMessageApiService, MessageApiService>();
builder.Services.AddScoped<IAuthApiService, AuthApiService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHubService, HubService>();

await builder.Build().RunAsync();
