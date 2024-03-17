using Application.Common.Interfaces;
using Application.Flights.Queries;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FlightsHttpClientService;

public class FlightsHttpClient(HttpClient httpClient, IOptions<ApiProvider> options, FlightXmlParser flightXmlParser, ILogger<FlightsHttpClient> logger) : IFlightsHttpClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ApiProvider _apiProvider = options.Value;

    private readonly ILogger<FlightsHttpClient> _logger = logger;

    private readonly FlightXmlParser _flightXmlParser = flightXmlParser;

    public async Task<List<FlightDto>> GetFlightsAsync()
    {
        var apiUrl = _apiProvider.ApiUrl;
        try
        {
            var xmlContent = await _httpClient.GetStringAsync(apiUrl);
            var response = _flightXmlParser.ParseFlightsXml(xmlContent);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting flights from the API");
            throw;
        }


    }
}