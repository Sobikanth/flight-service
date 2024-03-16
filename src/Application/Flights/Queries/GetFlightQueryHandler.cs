using Application.Common.Interfaces;

namespace Application.Flights.Queries;
public record GetFlightsQuery : IRequest<FlightResponse>
{
    public string? DepartureCity { get; init; } = string.Empty;
    public string? DestinationCity { get; init; } = string.Empty;
    public string? FlightNumber { get; init; } = string.Empty;

};


public class GetFlightsQueryHandler(IFlightsHttpClient flightsHttpClient) : IRequestHandler<GetFlightsQuery, FlightResponse>
{
    private readonly IFlightsHttpClient _flightsHttpClient = flightsHttpClient;

    public async Task<FlightResponse> Handle(GetFlightsQuery request, CancellationToken cancellationToken)
    {
        var allFlights = await _flightsHttpClient.GetFlightsAsync();

        var flightResponse = new FlightResponse
        {
            Flights = allFlights
        };

        foreach (var flight in flightResponse.Flights.ToList())
        {
            if (!string.IsNullOrEmpty(request.DepartureCity) && flight.DepartureCity != request.DepartureCity)
            {
                flightResponse.Flights.Remove(flight);
            }
            if (!string.IsNullOrEmpty(request.DestinationCity) && flight.DestinationCity != request.DestinationCity)
            {
                flightResponse.Flights.Remove(flight);
            }
            if (!string.IsNullOrEmpty(request.FlightNumber) && flight.FlightNumber != request.FlightNumber)
            {
                flightResponse.Flights.Remove(flight);
            }
        }

        return flightResponse;
    }
}