using Application.Flights.Queries;

namespace Application.Common.Interfaces;

public interface IFlightsHttpClient
{
    Task<List<FlightDto>> GetFlightsAsync();
}