using Moq;

using Application.Flights.Queries;
using FlightsHttpClientService;
using AutoMapper;

namespace XmlParserTests;

public class XmlParserTests
{

    [Fact]
#pragma warning disable CA1707 // Identifiers should not contain underscores
    public void ParseFlightsXml_WhenCalled_ReturnsListOfFlightDto()
#pragma warning restore CA1707 // Identifiers should not contain underscores
    {
        // Arrange
        var flightMapperMock = new Mock<IMapper>();
        flightMapperMock.Setup(m => m.Map<FlightDto>(It.IsAny<Flight>())).Returns(new FlightDto());
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

