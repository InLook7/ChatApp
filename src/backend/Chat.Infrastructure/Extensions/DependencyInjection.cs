using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Chat.Application.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Chat.Infrastructure.Auth;
using Chat.Infrastructure.Persistence;
using Chat.Infrastructure.Persistence.Data;
using Chat.Infrastructure.Analysis;

namespace Chat.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ChatDbConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISentimentAnalysis, SentimentAnalysis>();

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddSingleton(new TextAnalyticsClient(
            new Uri(configuration["TextAnalyticsEndpoint"]), 
            new AzureKeyCredential(configuration["TextAnalyticsApiKey"])
        ));

        return services;
    }
}