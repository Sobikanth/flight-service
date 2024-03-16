using System.Xml;
using System.Xml.Serialization;

using Application.Common.Interfaces;
using Application.Flights.Queries;

using AutoMapper;

namespace FlightsHttpClientService;

public class XmlParser(IMapper mapper) : IXmlParser
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
        var flightResponseDtos = new List<FlightDto>();
        if (flights?.Flight != null)
        {
            foreach (var flight in flights.Flight)
            {
                var flightDto = _mapper.Map<FlightDto>(flight);
                flightResponseDtos.Add(flightDto);
            }
        }
        return flightResponseDtos;
    }
}