using System.Xml.Linq;

using Application.Common.Interface;
using Application.Flights.Queries;

namespace FlightsHttpClient;

public class FlightsHttpClient(HttpClient httpClient) : IFlightsHttpClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<FlightsResponse> GetFlightsAsync()
    {
        string xmlContent = await _httpClient.GetStringAsync("https://flighttestservice.azurewebsites.net/flights");
        XDocument doc = XDocument.Parse(xmlContent);
        List<Flight> flights = doc.Root
            .Elements("Flight")
            .Select(e => new Flight
            {
                FlightNumber = (string)e.Element("FlightNumber"),
                DepartureCity = (string)e.Element("DepartureCity"),
                DestinationCity = (string)e.Element("DestinationCity"),
                DepartureTime = (DateTime)e.Element("DepartureTime"),
                ArrivalTime = (DateTime)e.Element("ArrivalTime")
            })
            .ToList();
        return new FlightsResponse { Flights = flights };
    }
}