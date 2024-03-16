using Application.Common.Interfaces;
using Application.Flights.Queries;

namespace Application.Common.Mappings;

public class FlightMapper : IFlightMapper
{
    public FlightDto Map(Flight flight)
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