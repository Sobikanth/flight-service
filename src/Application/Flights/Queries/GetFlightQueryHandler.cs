using Application.Common.Interfaces;

namespace Application.Flights.Queries;

public class GetFlightsQueryHandler(IFlightsHttpClient flightsHttpClient) : IRequestHandler<GetFlightQuery, FlightResponse>
{
    private readonly IFlightsHttpClient _flightsHttpClient = flightsHttpClient;

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
            if (!string.IsNullOrEmpty(request.FlightNumber) && flight.FlightNumber != request.FlightNumber)
            {
                flightResponse.Flights.Remove(flight);
            }
        }

        return flightResponse;
    }
}