// EraAgentDashboard.Infrastructure/DependencyInjection.cs
using AutoMapper; // Add this using
using EraAgentDashboard.Application.Interfaces;
using EraAgentDashboard.Infrastructure.HttpClients;
using EraAgentDashboard.Infrastructure.Mapping; // Make sure this is included if your profile is here
using EraAgentDashboard.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Reflection; // Add this using for Assembly

namespace EraAgentDashboard.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Configure strongly-typed settings
        var vapiSettingsSection = configuration.GetSection(VapiSettings.SectionName);
        services.Configure<VapiSettings>(vapiSettingsSection);

        // Validate settings on startup
        services.AddOptions<VapiSettings>()
            .Bind(vapiSettingsSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();


        // Configure HttpClient for VapiClient
        services.AddHttpClient<IVapiClient, VapiClient>((serviceProvider, client) =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<VapiSettings>>().Value;
            if (string.IsNullOrWhiteSpace(settings.BaseUrl) || string.IsNullOrWhiteSpace(settings.ApiKey))
            {
                throw new InvalidOperationException("Vapi API BaseUrl or ApiKey is not configured correctly.");
            }
            client.BaseAddress = new Uri(settings.BaseUrl);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", settings.ApiKey);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        });

        // Register AutoMapper
        // Scans the assembly containing this DependencyInjection class for classes inheriting Profile
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        // Or, if your profiles are in a different assembly:
        // services.AddAutoMapper(typeof(VapiMappingProfile).Assembly);


        // Register other Infrastructure services (e.g., repositories)
        // services.AddScoped<IYourRepository, YourRepository>();

        return services;
    }
}
