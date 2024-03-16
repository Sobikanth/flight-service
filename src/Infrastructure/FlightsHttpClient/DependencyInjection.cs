using Application;
using Application.Common.Interfaces;
using Application.Common.Mappings;

using FlightsHttpClient;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.FlightsHttpClient;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IFlightsHttpClient, FlightsHttpClient>();
        services.AddScoped<IFlightMapper, FlightMapper>();
        // services.AddSingleton<XmlParser>();
        services.AddScoped<IXmlParser, XmlParser>();
        services.Configure<ApiProvider>(configuration.GetSection(ApiProvider.APIPROVIDER));
        return services;
    }
}