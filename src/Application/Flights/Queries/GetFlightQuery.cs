using Microsoft.VisualBasic;

namespace Application.Flights.Queries;

public record GetFlightQuery : IRequest<FlightResponse>
{
    public string? DepartureCity { get; init; } = string.Empty;
    public string? DestinationCity { get; init; } = string.Empty;
    public DateTime? DepartureDate { get; init; }
    public DateTime? ArrivalDate { get; init; }
}