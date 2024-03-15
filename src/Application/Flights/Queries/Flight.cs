using System.Xml.Serialization;

namespace Application.Flights.Queries;


[XmlRoot(ElementName = "Flights")]
public class Flights
{
    [XmlElement(ElementName = "Flight")]
    public List<Flight> Flight { get; set; }
}

[XmlRoot(ElementName = "Flight")]
public class Flight
{
    [XmlElement(ElementName = "FlightNumber")]
    public string FlightNumber { get; set; }

    [XmlElement(ElementName = "DepartureCity")]
    public string DepartureCity { get; set; }

    [XmlElement(ElementName = "DestinationCity")]
    public string DestinationCity { get; set; }

    [XmlElement(ElementName = "DepartureTime")]
    public DateTime DepartureTime { get; set; }

    [XmlElement(ElementName = "ArrivalTime")]
    public DateTime ArrivalTime { get; set; }
}