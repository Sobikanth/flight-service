using Application;
using Application.Common.Interfaces;
using Application.Flights.Queries;

using Microsoft.Extensions.Options;

namespace Infrastructure.FlightsHttpClient;

public class FlightsHttpClient(HttpClient httpClient, IOptions<ApiProvider> options, IXmlParser xmlParser) : IFlightsHttpClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ApiProvider _apiProvider = options.Value;

    private readonly IXmlParser _xmlParser = xmlParser;

    public async Task<List<FlightDto>> GetFlightsAsync()
    {
        var apiUrl = _apiProvider.ApiUrl;
        var xmlContent = await _httpClient.GetStringAsync(apiUrl);

        var response = _xmlParser.ParseFlightsXml(xmlContent);

        return response;
    }
}