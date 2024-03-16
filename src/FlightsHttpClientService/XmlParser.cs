using System.Xml;
using System.Xml.Serialization;

using Application.Common.Interfaces;
using Application.Flights.Queries;

namespace FlightsHttpClientService;

public class XmlParser(IFlightMapper flightMapper) : IXmlParser
{
    private readonly IFlightMapper _flightMapper = flightMapper;

    public List<FlightDto> ParseFlightsXml(string xmlContent)
    {
        var serializer = new XmlSerializer(typeof(Flights));
        using var reader = new StringReader(xmlContent);
        var xmlReaderSettings = new XmlReaderSettings
        {
            DtdProcessing = DtdProcessing.Prohibit,
            XmlResolver = null
        };
        using var xmlReader = XmlReader.Create(reader, xmlReaderSettings);
        var flights = serializer.Deserialize(xmlReader) as Flights;
        var flightResponseDtos = new List<FlightDto>();
        if (flights?.Flight != null)
        {
            foreach (var flight in flights.Flight)
            {
                flightResponseDtos.Add(_flightMapper.Map(flight));
            }
        }
        return flightResponseDtos;
    }
}