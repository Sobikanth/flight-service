using Application.Common.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace FlightsHttpClient;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
    {
        services.AddHttpClient<IFlightsHttpClient, FlightsHttpClient>();
        return services;
    }
}