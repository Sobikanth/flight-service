using System.Globalization;

using Application.Common.Interfaces;
using Application.Flights.Queries;

using Microsoft.Extensions.Logging;

using Moq;

using Newtonsoft.Json;

namespace GetFlightQueryHandlerTests;

public class GetFlightQueryHandlerTests
{
    private readonly Mock<IFlightsHttpClient> _flightsHttpClientMock;
    private readonly Mock<ILogger<GetFlightsQueryHandler>> _loggerMock;

    public GetFlightQueryHandlerTests()
    {
        _flightsHttpClientMock = new Mock<IFlightsHttpClient>();
        _loggerMock = new Mock<ILogger<GetFlightsQueryHandler>>();

        var json = File.ReadAllText("flights.json");
        var flightDtos = JsonConvert.DeserializeObject<FlightDto[]>(json);
        var flightDtosList = flightDtos != null ? new List<FlightDto>(flightDtos) : [];

        var flightResponse = new FlightResponse
        {
            Flights = flightDtosList
        };
        _flightsHttpClientMock.Setup(m => m.GetFlightsAsync()).ReturnsAsync(flightResponse.Flights);

    }
    [Fact]
    public void GetFlightQueryHandler_ShouldReturnAllFlightResponse_IfNoQueryParametersAreGiven()
    {
        var getFlightQuery = new GetFlightQuery();
        var getFlightsQueryHandler = new GetFlightsQueryHandler(_flightsHttpClientMock.Object, _loggerMock.Object);

        // Act
        var result = getFlightsQueryHandler.Handle(getFlightQuery, CancellationToken.None).Result;

        // Assert
        Assert.Equal(4, result.Flights.Count);
    }

    [Fact]
    public void GetFlightHandler_ShouldReturnFilteredFlights_WhenDepartureCityAndDestinationCityIsGiven()
    {
        // Arrange

        var getFlightQuery = new GetFlightQuery
        {
            DepartureCity = "City3",
            DestinationCity = "City4"
        };
        var getFlightsQueryHandler = new GetFlightsQueryHandler(_flightsHttpClientMock.Object, _loggerMock.Object);

        // Act
        var result = getFlightsQueryHandler.Handle(getFlightQuery, CancellationToken.None).Result;

        // Assert
        Assert.Equal(getFlightQuery.DepartureCity, result.Flights[0].DepartureCity);
        Assert.Equal(getFlightQuery.DestinationCity, result.Flights[0].DestinationCity);
    }

    [Fact]
    public void GetFlightHandler_ShouldReturnFilteredFlights_WhenDepartureDateAndArrivalDateIsGiven()
    {
        // Arrange

        var getFlightQuery = new GetFlightQuery
        {
            DepartureDate = DateTime.ParseExact("2023-03-21", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            ArrivalDate = DateTime.ParseExact("2023-03-21", "yyyy-MM-dd", CultureInfo.InvariantCulture)
        };
        var getFlightsQueryHandler = new GetFlightsQueryHandler(_flightsHttpClientMock.Object, _loggerMock.Object);

        // Act
        var result = getFlightsQueryHandler.Handle(getFlightQuery, CancellationToken.None).Result;

        // Assert
        Assert.Equal(getFlightQuery.DepartureDate, result.Flights[0].DepartureTime.Date);
        Assert.Equal(getFlightQuery.ArrivalDate, result.Flights[0].ArrivalTime.Date);
    }

    [Fact]
    public void GetFlightHandler_ShouldReturnFilteredFlights_WhenAllQueryParametersAreGiven()
    {
        // Arrange

        var getFlightQuery = new GetFlightQuery
        {
            DepartureCity = "City3",
            DestinationCity = "City4",
            DepartureDate = DateTime.ParseExact("2023-03-22", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            ArrivalDate = DateTime.ParseExact("2023-03-22", "yyyy-MM-dd", CultureInfo.InvariantCulture)
        };
        var getFlightsQueryHandler = new GetFlightsQueryHandler(_flightsHttpClientMock.Object, _loggerMock.Object);

        // Act
        var result = getFlightsQueryHandler.Handle(getFlightQuery, CancellationToken.None).Result;

        // Assert
        Assert.Equal(getFlightQuery.DepartureCity, result.Flights[0].DepartureCity);
        Assert.Equal(getFlightQuery.DestinationCity, result.Flights[0].DestinationCity);
        Assert.Equal(getFlightQuery.DepartureDate, result.Flights[0].DepartureTime.Date);
        Assert.Equal(getFlightQuery.ArrivalDate, result.Flights[0].ArrivalTime.Date);
    }

    [Fact]
    public void GetFlightHandler_ShouldReturnEmptyFlights_WhenNoFlightMatchesQueryParameters()
    {
        // Arrange

        var getFlightQuery = new GetFlightQuery
        {
            DepartureCity = "City34",
            DestinationCity = "City44",
            DepartureDate = DateTime.ParseExact("2023-03-20", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            ArrivalDate = DateTime.ParseExact("2023-03-20", "yyyy-MM-dd", CultureInfo.InvariantCulture)
        };
        var getFlightsQueryHandler = new GetFlightsQueryHandler(_flightsHttpClientMock.Object, _loggerMock.Object);

        // Act
        var result = getFlightsQueryHandler.Handle(getFlightQuery, CancellationToken.None).Result;

        // Assert
        Assert.Empty(result.Flights);
    }

    [Fact]
    public void GetFlightHandler_ShouldReturnMultipleFlights_WhenMultipleFlightsMatchQueryParameters()
    {
        // Arrange

        var getFlightQuery = new GetFlightQuery
        {
            DepartureCity = "City3",
            DestinationCity = "City4"
        };
        var getFlightsQueryHandler = new GetFlightsQueryHandler(_flightsHttpClientMock.Object, _loggerMock.Object);

        // Act
        var result = getFlightsQueryHandler.Handle(getFlightQuery, CancellationToken.None).Result;

        // Assert
        Assert.Equal(2, result.Flights.Count);
        Assert.Equal(getFlightQuery.DepartureCity, result.Flights[0].DepartureCity);
        Assert.Equal(getFlightQuery.DestinationCity, result.Flights[0].DestinationCity);
        Assert.Equal(getFlightQuery.DepartureCity, result.Flights[1].DepartureCity);
        Assert.Equal(getFlightQuery.DestinationCity, result.Flights[1].DestinationCity);
    }
}
