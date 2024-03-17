using Moq;

using Application.Flights.Queries;
using FlightsHttpClientService;
using AutoMapper;

namespace XmlParserTests;

public class XmlParserTests
{

    [Fact]
    public void ParseFlightsXml_ShouldReturnFlightDtos_WhenXmlContentIsGiven()
    {
        // Arrange
        var flightMapperMock = new Mock<IMapper>();
        flightMapperMock.Setup(m => m.Map<List<FlightDto>>(It.IsAny<List<Flight>>()))
            .Returns((List<Flight> flights) => flights.Select(flight => new FlightDto
            {
                FlightNumber = flight.FlightNumber,
                DepartureCity = flight.DepartureCity,
                DestinationCity = flight.DestinationCity,
                DepartureTime = flight.DepartureTime,
                ArrivalTime = flight.ArrivalTime
            }).ToList());

        var flightXmlParser = new FlightXmlParser(flightMapperMock.Object);
        var xmlContent = File.ReadAllText("flights.xml");

        // Act
        var result = flightXmlParser.ParseFlightsXml(xmlContent);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.IsType<List<FlightDto>>(result);
        Assert.Equal(10, result.Count);
    }
}

