using Application.Flights.Queries;

namespace Application.Common.Mappings;

public class FlightMapper
{
    public static FlightDto Map(Flight flight)
    {
        return new FlightDto
        {
            FlightNumber = flight.FlightNumber,
            DepartureCity = flight.DepartureCity,
            DestinationCity = flight.DestinationCity,
            DepartureTime = flight.DepartureTime,
            ArrivalTime = flight.ArrivalTime
        };
    }
}