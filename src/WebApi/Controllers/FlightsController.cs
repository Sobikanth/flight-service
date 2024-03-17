using Application.Flights.Queries;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController(ILogger<FlightsController> logger) : ControllerBase
{
    private readonly ILogger<FlightsController> _logger = logger;

    [HttpGet]
    public async Task<ActionResult<FlightResponse>> GetFlights(
        [FromServices] ISender sender,
        [FromQuery] GetFlightQuery query)
    {
        _logger.LogInformation($"GetFlights endpoint called with query: {query} at {DateTime.UtcNow}");
        var response = await sender.Send(query);
        return response;
    }
}