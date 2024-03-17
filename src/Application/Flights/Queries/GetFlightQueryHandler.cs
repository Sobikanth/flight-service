using Application.Common.Interfaces;

using Microsoft.Extensions.Logging;

namespace Application.Flights.Queries;

public class GetFlightsQueryHandler(IFlightsHttpClient flightsHttpClient, ILogger<GetFlightsQueryHandler> logger) : IRequestHandler<GetFlightQuery, FlightResponse>
{
    private readonly IFlightsHttpClient _flightsHttpClient = flightsHttpClient;
    private readonly ILogger<GetFlightsQueryHandler> _logger = logger;

    public async Task<FlightResponse> Handle(GetFlightQuery request, CancellationToken cancellationToken)
    {
        var allFlights = await _flightsHttpClient.GetFlightsAsync(cancellationToken);

        var flightResponse = new FlightResponse
        {
            Flights = allFlights
        };

        // Filter the flightResponse based on the request query parameters
        flightResponse.Flights = flightResponse.Flights
            .Where(flight =>
                (string.IsNullOrEmpty(request.DepartureCity) || flight.DepartureCity == request.DepartureCity) &&
                (string.IsNullOrEmpty(request.DestinationCity) || flight.DestinationCity == request.DestinationCity) &&
                (!request.DepartureDate.HasValue || flight.DepartureTime.Date == request.DepartureDate.Value.Date) &&
                (!request.ArrivalDate.HasValue || flight.ArrivalTime.Date == request.ArrivalDate.Value.Date))
            .ToList();
        _logger.LogInformation("Returning {Count} flights", flightResponse.Flights.Count);

        return flightResponse;
    }
}