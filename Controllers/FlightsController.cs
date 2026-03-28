using AeromexicoPrueba.Contracts.Requests;
using AeromexicoPrueba.Services;
using Microsoft.AspNetCore.Mvc;

namespace AeromexicoPrueba.Controllers;

[ApiController]
[Route("api/flights")]
public sealed class FlightsController : ControllerBase
{
    private readonly IFlightService _flightService;

    public FlightsController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    [HttpGet]
    public IActionResult GetFlights([FromQuery] GetFlightsRequest request)
    {
        var response = _flightService.GetFlights(request);
        return Ok(response);
    }
}
