using Application.Flights.Queries;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<FlightResponse>> GetFlights(
        [FromServices] ISender sender,
        [FromQuery] GetFlightQuery query)
    {
        var response = await sender.Send(query);
        return response;
    }
}