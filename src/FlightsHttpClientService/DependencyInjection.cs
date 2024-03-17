using Application.Common.Interfaces;


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlightsHttpClientService;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IFlightsHttpClient, FlightsHttpClient>();
        services.AddScoped<FlightXmlParser>();
        services.Configure<ApiProvider>(configuration.GetSection(ApiProvider.APIPROVIDER));
        return services;
    }
}