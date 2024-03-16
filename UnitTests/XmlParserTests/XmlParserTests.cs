using Moq;
using Application.Common.Interfaces;

using Application.Flights.Queries;
using FlightsHttpClientService;

namespace XmlParserTests;

public class XmlParserTests
{

    [Fact]
#pragma warning disable CA1707 // Identifiers should not contain underscores
    public void ParseFlightsXml_WhenCalled_ReturnsListOfFlightDto()
#pragma warning restore CA1707 // Identifiers should not contain underscores
    {
        // Arrange
        var flightMapperMock = new Mock<IFlightMapper>();
        // flightMapperMock.Setup(m => m.Map(It.IsAny<Flight>())).Returns((Flight flight) =>
        // {
        //     return new FlightDto
        //     {
        //         FlightNumber = flight.FlightNumber,
        //         DepartureCity = flight.DepartureCity,
        //         DestinationCity = flight.DestinationCity,
        //         DepartureTime = flight.DepartureTime,
        //         ArrivalTime = flight.ArrivalTime
        //     };
        // });
        var xmlParser = new XmlParser(flightMapperMock.Object);
        var xmlContent = File.ReadAllText("flights.xml");

        // Act
        var result = xmlParser.ParseFlightsXml(xmlContent);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.IsType<List<FlightDto>>(result);
        Assert.Equal(10, result.Count);
    }
}

