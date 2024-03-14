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

    public async Task<FlightsResponse> GetFlights(ISender sender, [FromQuery] string? departureCity,
            [FromQuery] string? destinationCity, CancellationToken cancellationToken)
    {
        var query = new GetFlightsQuery
        {
            DepartureCity = departureCity,
            DestinationCity = destinationCity
        };
        var response = await sender.Send(query, cancellationToken);
        if (response.Flights == null)
        {
            return new FlightsResponse { Flights = [] };
        }
        return response;
    }
}