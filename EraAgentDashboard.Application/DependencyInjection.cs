using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EraAgentDashboard.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // services.AddAutoMapper(Assembly.GetExecutingAssembly()); // If using AutoMapper
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        // Register other Application services if any
        return services;
    }
}
