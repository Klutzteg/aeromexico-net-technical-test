using AeromexicoPrueba.Domain;

namespace AeromexicoPrueba.Store;

public sealed class InMemoryStore
{
    private readonly object _lock = new();
    private int _passengerSequence = 0;
    private int _reservationSequence = 0;

    public List<Flight> Flights { get; } =
    [
        new() { Id = 1, FlightNumber = "AM10", Origin = "MX", Destination = "NY", DepartureDate = new DateTime(2026, 3, 28, 8, 0, 0) },
        new() { Id = 2, FlightNumber = "AM20", Origin = "GD", Destination = "MT", DepartureDate = new DateTime(2026, 3, 29, 10, 30, 0) },
        new() { Id = 3, FlightNumber = "AM30", Origin = "CD", Destination = "CA", DepartureDate = new DateTime(2026, 3, 30, 15, 45, 0) },
        new() { Id = 4, FlightNumber = "AM40", Origin = "TI", Destination = "MO", DepartureDate = new DateTime(2026, 3, 31, 21, 15, 0) }
    ];

    public List<Passenger> Passengers { get; } = [];
    public List<Reservation> Reservations { get; } = [];

    public int NextPassengerId()
    {
        lock (_lock)
        {
            _passengerSequence++;
            return _passengerSequence;
        }
    }

    public int NextReservationId()
    {
        lock (_lock)
        {
            _reservationSequence++;
            return _reservationSequence;
        }
    }
}
