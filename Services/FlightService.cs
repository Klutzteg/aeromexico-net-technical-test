using System.Globalization;
using AeromexicoPrueba.Contracts.Requests;
using AeromexicoPrueba.Contracts.Responses;
using AeromexicoPrueba.Store;

namespace AeromexicoPrueba.Services;

public sealed class FlightService : IFlightService
{
    private const string DateFormat = "yyyy/MM/dd HH:mm:ss";
    private readonly InMemoryStore _store;

    public FlightService(InMemoryStore store)
    {
        _store = store;
    }

    public IReadOnlyCollection<FlightResponse> GetFlights(GetFlightsRequest request)
    {
        var startDate = DateTime.ParseExact(request.StartDate, DateFormat, CultureInfo.InvariantCulture);
        var endDate = DateTime.ParseExact(request.EndDate, DateFormat, CultureInfo.InvariantCulture);

        return _store.Flights
            .Where(flight => flight.DepartureDate >= startDate && flight.DepartureDate <= endDate)
            .OrderBy(flight => flight.DepartureDate)
            .Select(flight => new FlightResponse
            {
                Id = flight.Id,
                FlightNumber = flight.FlightNumber,
                Origin = flight.Origin,
                Destination = flight.Destination,
                DepartureDate = flight.DepartureDate.ToString(DateFormat)
            })
            .ToList();
    }
}
