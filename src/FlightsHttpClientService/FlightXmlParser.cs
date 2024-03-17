using System.Xml;
using System.Xml.Serialization;

using Application.Common.Interfaces;
using Application.Flights.Queries;

using AutoMapper;

namespace FlightsHttpClientService;

public class FlightXmlParser(IMapper mapper) : IFlightXmlParser
{
    private readonly IMapper _mapper = mapper;

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
        var flightResponseDtos = _mapper.Map<List<FlightDto>>(flights?.Flight);
        return flightResponseDtos;
    }
}