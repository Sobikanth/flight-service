using Application.Flights.Queries;

namespace Application.Common.Interfaces;

public interface IFlightMapper
{
    public FlightDto Map(Flight flight);
}