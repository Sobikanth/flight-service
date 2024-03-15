using Application;
using Application.Common.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.FlightsHttpClient;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IFlightsHttpClient, FlightsHttpClient>();
        services.Configure<ApiProvider>(configuration.GetSection(ApiProvider.APIPROVIDER));
        return services;
    }
}