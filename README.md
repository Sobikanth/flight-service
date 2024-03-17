# FlightService

## Introduction

FlightService is a service that provides a list of flights based on various query parameters. This document provides instructions on how to set up and run FlightService using Visual Studio Code.

## Getting Started with Visual Studio Code

### Prerequisites

- Visual Studio Code installed on your system
- .NET Core SDK installed on your system

### Setup

1. Open the project in Visual Studio Code.
2. Open a terminal in Visual Studio Code.
3. Run the following command to install the required packages:

```bash
dotnet run
```

4. Navigate to the following URL in your browser to view the Swagger documentation:

```bash
http://localhost:5157/index.html
```

5. To view all flights leave the query parameters empty and click on the "Try it out" button.

6. To view flights based on query parameters, enter the required parameters and click on the "Try it out" button.

**Query parameters**:

1. **DepartureCity** : The city from which the flight departs. (e.g., "London")
2. **ArrivalCity** : The city to which the flight arrives. (e.g., "Paris")
3. **DepartureDate** : The date on which the flight departs. (e.g., "2023-11-22")
4. **ArrivalDate** : The date on which the flight arrives. (e.g., "2023-11-22")

### Usage on Postman

To get all flights, you can send a GET request to the following endpoint:

```bash
http://localhost:5157/api/flights
```

Given below is an example of a GET request with all query parameters:

```bash
http://localhost:5157/api/flights?DepartureCity=London&ArrivalCity=Paris&DepartureDate=2023-11-22&ArrivalDate=2023-11-22
```
