using Application.Common.Interfaces;

namespace Application.Flights.Queries;
public record GetFlightsQuery : IRequest<FlightsResponse>
{
    public string DepartureCity { get; init; } = string.Empty;
    public string DestinationCity { get; init; } = string.Empty;

};


public class GetFlightsQueryHandler(IFlightsHttpClient flightsHttpClient) : IRequestHandler<GetFlightsQuery, FlightsResponse>
{
    private readonly IFlightsHttpClient _flightsHttpClient = flightsHttpClient;

    public async Task<FlightsResponse> Handle(GetFlightsQuery request, CancellationToken cancellationToken)
    {
        var allFlights = await _flightsHttpClient.GetFlightsAsync();
        if (allFlights?.Flights == null)
        {
            return new FlightsResponse { Flights = [] };
        }
        var filteredFlights = allFlights.Flights
            .Where(f => (string.IsNullOrEmpty(request.DepartureCity) || f.DepartureCity == request.DepartureCity) &&
                        (string.IsNullOrEmpty(request.DestinationCity) || f.DestinationCity == request.DestinationCity))
            .ToList();

        return new FlightsResponse { Flights = filteredFlights };
    }
}