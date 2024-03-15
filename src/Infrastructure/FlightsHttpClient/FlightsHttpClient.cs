using System.Xml;
using System.Xml.Serialization;

using Application;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Flights.Queries;

using Microsoft.Extensions.Options;

namespace Infrastructure.FlightsHttpClient;

public class FlightsHttpClient(HttpClient httpClient, IOptions<ApiProvider> options) : IFlightsHttpClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ApiProvider _apiProvider = options.Value;

    public async Task<List<FlightDto>> GetFlightsAsync()
    {
        var apiUrl = _apiProvider.ApiUrl;
        var xmlContent = await _httpClient.GetStringAsync(apiUrl);
        var serializer = new XmlSerializer(typeof(Flights));

        using var reader = new StringReader(xmlContent);

        var xmlReaderSettings = new XmlReaderSettings
        {
            DtdProcessing = DtdProcessing.Prohibit,
            XmlResolver = null
        };

        using var xmlReader = XmlReader.Create(reader, xmlReaderSettings);
        var flights = (Flights)serializer.Deserialize(xmlReader);

        var flightResponseDtos = new List<FlightDto>();

        foreach (var flight in flights.Flight)
        {
            flightResponseDtos.Add(FlightMapper.Map(flight));
        }

        return flightResponseDtos;
    }
}