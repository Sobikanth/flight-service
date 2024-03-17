using Application.Flights.Queries;

namespace Application.Common.Interfaces;

public interface IFlightXmlParser
{
    public List<FlightDto> ParseFlightsXml(string xmlContent);
}