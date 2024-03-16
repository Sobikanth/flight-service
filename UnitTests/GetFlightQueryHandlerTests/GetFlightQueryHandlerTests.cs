using Application.Common.Interfaces;
using Application.Flights.Queries;

using Moq;

using Newtonsoft.Json;

namespace GetFlightQueryHandlerTests;

public class GetFlightQueryHandlerTests
{
    [Fact]
#pragma warning disable CA1707 // Identifiers should not contain underscores
    public void GetFlightQueryHandler_ShouldReturnAllFlightResponse_If_No_Query_Parameters_Are_Given()
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

        var getFlightQuery = new GetFlightQuery();
        var getFlightsQueryHandler = new GetFlightsQueryHandler(flightsHttpClientMock.Object);

        // Act
        var result = getFlightsQueryHandler.Handle(getFlightQuery, CancellationToken.None).Result;

        // Assert
        Assert.Equal(3, result.Flights.Count);
    }

    [Fact]
    public void GetFlightHandler_Should_Return_Filtered_Flights()
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

        var getFlightQuery = new GetFlightQuery
        {
            DepartureCity = "City3",
            DestinationCity = "City4"
        };
        var getFlightsQueryHandler = new GetFlightsQueryHandler(flightsHttpClientMock.Object);

        // Act
        var result = getFlightsQueryHandler.Handle(getFlightQuery, CancellationToken.None).Result;

        // Assert
        Assert.Equal(getFlightQuery.DepartureCity, result.Flights[0].DepartureCity);
        Assert.Equal(getFlightQuery.DestinationCity, result.Flights[0].DestinationCity);
    }

    [Fact]
    public void GetFlightHandler_Should_Return_Filtered_Flights_If_FlightNumber_Is_Given()
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

        var getFlightQuery = new GetFlightQuery
        {
            FlightNumber = "ABC123"
        };
        var getFlightsQueryHandler = new GetFlightsQueryHandler(flightsHttpClientMock.Object);

        // Act
        var result = getFlightsQueryHandler.Handle(getFlightQuery, CancellationToken.None).Result;

        // Assert
        Assert.Equal(getFlightQuery.FlightNumber, result.Flights[0].FlightNumber);
    }

    [Fact]
    public void GetFlightHandler_Should_Return_Empty_Flights_If_No_Matching_FlightNumber_Is_Found()
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

        var getFlightQuery = new GetFlightQuery
        {
            FlightNumber = "F5"
        };
        var getFlightsQueryHandler = new GetFlightsQueryHandler(flightsHttpClientMock.Object);

        // Act
        var result = getFlightsQueryHandler.Handle(getFlightQuery, CancellationToken.None).Result;

        // Assert
        Assert.Empty(result.Flights);
    }
}
