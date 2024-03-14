using Application.Flights.Queries;
using WebApi.Infrastructure;

namespace WebApi.Endpoints;

public class Flights : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
        .MapGet(GetFlights);
    }

    public async Task<FlightsResponse> GetFlights(ISender sender, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetFlightsQuery(), cancellationToken);
    }
}