using Application.Flights.Queries;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints;

public class Flights : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
        .MapGet(GetFlights);
    }

    public async Task<FlightResponse> GetFlights(ISender sender, [FromQuery] string? departureCity,
            [FromQuery] string? destinationCity, [FromQuery] string? flightNumber, CancellationToken cancellationToken)
    {
        var query = new GetFlightsQuery
        {
            DepartureCity = departureCity,
            DestinationCity = destinationCity,
            FlightNumber = flightNumber
        };
        var response = await sender.Send(query, cancellationToken);
        return response;
    }
}