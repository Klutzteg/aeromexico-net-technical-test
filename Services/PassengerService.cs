using AeromexicoPrueba.Contracts.Requests;
using AeromexicoPrueba.Contracts.Responses;
using AeromexicoPrueba.Domain;
using AeromexicoPrueba.Store;

namespace AeromexicoPrueba.Services;

public sealed class PassengerService : IPassengerService
{
    private readonly InMemoryStore _store;

    public PassengerService(InMemoryStore store)
    {
        _store = store;
    }

    public PassengerResponse CreatePassenger(CreatePassengerRequest request)
    {
        var passenger = new Passenger
        {
            Id = _store.NextPassengerId(),
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim()
        };

        _store.Passengers.Add(passenger);

        return new PassengerResponse
        {
            Id = passenger.Id,
            FirstName = passenger.FirstName,
            LastName = passenger.LastName
        };
    }
}
