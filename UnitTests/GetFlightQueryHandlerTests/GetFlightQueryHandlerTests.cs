using Application.Common.Interfaces;
using Application.Flights.Queries;

using Moq;

using Newtonsoft.Json;

namespace GetFlightQueryHandlerTests;

public class GetFlightQueryHandlerTests
{
    [Fact]
#pragma warning disable CA1707 // Identifiers should not contain underscores
    public void GetFlightQueryHandler_ShouldReturnFlightResponse()
#pragma warning restore CA1707 // Identifiers should not contain underscores
    {
        // Arrange
        var flightsHttpClientMock = new Mock<IFlightsHttpClient>();

        var json = File.ReadAllText("flights.json");
        var flightDtos = JsonConvert.DeserializeObject<FlightDto[]>(json);

        var flightResponse = new FlightResponse
        {
            Flights = [.. flightDtos]
        };
        flightsHttpClientMock.Setup(m => m.GetFlightsAsync()).ReturnsAsync(flightResponse.Flights);

        var getFlightsQuery = new GetFlightsQuery();
        var getFlightsQueryHandler = new GetFlightsQueryHandler(flightsHttpClientMock.Object);

        // Act
        var result = getFlightsQueryHandler.Handle(getFlightsQuery, CancellationToken.None).Result;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<FlightResponse>(result);
        Assert.Equal(3, result.Flights.Count);
    }
}