using Application.Common.Interfaces;

using Microsoft.Extensions.Logging;

namespace Application.Flights.Queries;

public class GetFlightsQueryHandler(IFlightsHttpClient flightsHttpClient, ILogger<GetFlightsQueryHandler> logger) : IRequestHandler<GetFlightQuery, FlightResponse>
{
    private readonly IFlightsHttpClient _flightsHttpClient = flightsHttpClient;
    private readonly ILogger<GetFlightsQueryHandler> _logger = logger;

    public async Task<FlightResponse> Handle(GetFlightQuery request, CancellationToken cancellationToken)
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
            if (request.DepartureDate.HasValue && flight.DepartureTime.Date != request.DepartureDate.Value.Date)
            {
                flightResponse.Flights.Remove(flight);
            }
            if (request.ArrivalDate.HasValue && flight.ArrivalTime.Date != request.ArrivalDate.Value.Date)
            {
                flightResponse.Flights.Remove(flight);
            }
        }
        _logger.LogInformation("Returning {Count} flights", flightResponse.Flights.Count);

        return flightResponse;
    }
}