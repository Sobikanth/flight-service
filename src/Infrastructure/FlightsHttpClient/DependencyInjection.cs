using Application.Common.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.FlightsHttpClient;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
    {
        services.AddHttpClient<IFlightsHttpClient, FlightsHttpClient>();
        return services;
    }
}