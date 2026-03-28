using AeromexicoPrueba.Contracts.Requests;
using AeromexicoPrueba.Contracts.Responses;

namespace AeromexicoPrueba.Services;

public interface IFlightService
{
    IReadOnlyCollection<FlightResponse> GetFlights(GetFlightsRequest request);
}
