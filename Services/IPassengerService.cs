using AeromexicoPrueba.Contracts.Requests;
using AeromexicoPrueba.Contracts.Responses;

namespace AeromexicoPrueba.Services;

public interface IPassengerService
{
    PassengerResponse CreatePassenger(CreatePassengerRequest request);
}
