using Application.Flights.Queries;

namespace Application.Common.Interface;

public interface IFlightsHttpClient
{
    Task<FlightsResponse> GetFlightsAsync();

}