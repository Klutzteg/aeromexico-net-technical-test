using AeromexicoPrueba.Contracts.Requests;
using AeromexicoPrueba.Contracts.Responses;

namespace AeromexicoPrueba.Services;

public interface IReservationService
{
    ReservationResponse CreateReservation(CreateReservationRequest request);
}
