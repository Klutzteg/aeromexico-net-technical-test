using AeromexicoPrueba.Contracts.Requests;
using AeromexicoPrueba.Services;
using Microsoft.AspNetCore.Mvc;

namespace AeromexicoPrueba.Controllers;

[ApiController]
[Route("api/passengers")]
public sealed class PassengersController : ControllerBase
{
    private readonly IPassengerService _passengerService;

    public PassengersController(IPassengerService passengerService)
    {
        _passengerService = passengerService;
    }

    [HttpPost]
    public IActionResult CreatePassenger([FromBody] CreatePassengerRequest request)
    {
        var response = _passengerService.CreatePassenger(request);
        return CreatedAtAction(nameof(CreatePassenger), new { id = response.Id }, response);
    }
}
