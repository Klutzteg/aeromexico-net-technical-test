using AeromexicoPrueba.Contracts.Requests;
using AeromexicoPrueba.Contracts.Responses;
using AeromexicoPrueba.Domain;
using AeromexicoPrueba.Store;

namespace AeromexicoPrueba.Services;

public sealed class ReservationService : IReservationService
{
    private readonly InMemoryStore _store;

    public ReservationService(InMemoryStore store)
    {
        _store = store;
    }

    public ReservationResponse CreateReservation(CreateReservationRequest request)
    {
        var flightsExist = request.Flights.All(flightId => _store.Flights.Any(flight => flight.Id == flightId));
        if (!flightsExist)
        {
            throw new InvalidOperationException("One or more flights do not exist.");
        }

        var passengersExist = request.Passengers.All(passengerId => _store.Passengers.Any(passenger => passenger.Id == passengerId));
        if (!passengersExist)
        {
            throw new InvalidOperationException("One or more passengers do not exist.");
        }

        var reservation = new Reservation
        {
            Id = _store.NextReservationId(),
            FlightIds = request.Flights.Distinct().ToList(),
            PassengerIds = request.Passengers.Distinct().ToList(),
            CreatedAt = DateTime.UtcNow
        };

        _store.Reservations.Add(reservation);

        return new ReservationResponse
        {
            Id = reservation.Id,
            Flights = reservation.FlightIds,
            Passengers = reservation.PassengerIds,
            CreatedAt = reservation.CreatedAt.ToString("yyyy/MM/dd HH:mm:ss")
        };
    }
}
