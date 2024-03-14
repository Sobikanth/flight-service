using Application.Common.Interface;

namespace Application.Flights.Queries;
public record GetFlightsQuery : IRequest<FlightsResponse>;


public class GetFlightsQueryHandler : IRequestHandler<GetFlightsQuery, FlightsResponse>
{
    private readonly IFlightsHttpClient _flightsHttpClient;
    public GetFlightsQueryHandler(IFlightsHttpClient flightsHttpClient)
    {
        _flightsHttpClient = flightsHttpClient;
    }
    public async Task<FlightsResponse> Handle(GetFlightsQuery request, CancellationToken cancellationToken)
    {
        return await _flightsHttpClient.GetFlightsAsync();
    }
}