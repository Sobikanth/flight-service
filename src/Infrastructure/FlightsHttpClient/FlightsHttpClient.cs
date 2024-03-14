using System.Xml.Linq;

using Application.Common.Interfaces;
using Application.Flights.Queries;

namespace Infrastructure.FlightsHttpClient;

public class FlightsHttpClient(HttpClient httpClient) : IFlightsHttpClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<FlightsResponse> GetFlightsAsync()
    {
        try
        {
            var xmlContent = await _httpClient.GetStringAsync("https://flighttestservice.azurewebsites.net/flights");
            var doc = XDocument.Parse(xmlContent);
            var flights = doc.Root
                .Elements("Flight")
                .Select(e => new Flight
                {
                    FlightNumber = (string?)e.Element("FlightNumber") ?? string.Empty,
                    DepartureCity = (string?)e.Element("DepartureCity") ?? string.Empty,
                    DestinationCity = (string?)e.Element("DestinationCity") ?? string.Empty,
                    DepartureTime = (DateTime?)e.Element("DepartureTime") ?? DateTime.MinValue,
                    ArrivalTime = (DateTime?)e.Element("ArrivalTime") ?? DateTime.MinValue
                })
                .ToList();
            return new FlightsResponse { Flights = flights };
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving flights", ex);
        }
    }
}