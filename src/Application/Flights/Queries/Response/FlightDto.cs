namespace Application.Flights.Queries;

public class FlightDto
{
    public string FlightNumber { get; set; }
    public string DepartureCity { get; set; }
    public string DestinationCity { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
}