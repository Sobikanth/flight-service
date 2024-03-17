using Application.Flights.Queries;

using AutoMapper;

namespace Application.Common.Mappings;

public class FlightProfile : Profile
{
    public FlightProfile()
    {
        CreateMap<Flight, FlightDto>();
    }
}