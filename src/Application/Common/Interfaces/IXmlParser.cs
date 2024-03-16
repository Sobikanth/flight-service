using Application.Flights.Queries;

namespace Application.Common.Interfaces;

public interface IXmlParser
{
    public List<FlightDto> ParseFlightsXml(string xmlContent);
}