namespace Application.Flights.Queries;

public class FlightsResponse
{
    public List<Flight>? Flights { get; set; }
}
public class Flight
{
    public string? FlightNumber { get; set; }
    public string? DepartureCity { get; set; }
    public string? DestinationCity { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
}