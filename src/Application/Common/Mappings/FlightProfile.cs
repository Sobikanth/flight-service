using Application.Flights.Queries;

using AutoMapper;

namespace Application.Common.Mappings;

public class FlightProfile : Profile
{
    public FlightProfile()
    {
        CreateMap<Flight, FlightDto>()
        .ForMember(d => d.FlightNumber, opt => opt.MapFrom(s => s.FlightNumber))
        .ForMember(d => d.DepartureCity, opt => opt.MapFrom(s => s.DepartureCity))
        .ForMember(d => d.DestinationCity, opt => opt.MapFrom(s => s.DestinationCity))
        .ForMember(d => d.DepartureTime, opt => opt.MapFrom(s => s.DepartureTime))
        .ForMember(d => d.ArrivalTime, opt => opt.MapFrom(s => s.ArrivalTime));
    }
}