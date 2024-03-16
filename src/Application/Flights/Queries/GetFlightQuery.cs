namespace Application.Flights.Queries;

public record GetFlightQuery : IRequest<FlightResponse>
{
    public string? DepartureCity { get; init; } = string.Empty;
    public string? DestinationCity { get; init; } = string.Empty;
    public string? FlightNumber { get; init; } = string.Empty;
}