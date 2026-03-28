using AeromexicoPrueba.Contracts.Requests;
using AeromexicoPrueba.Services;
using Microsoft.AspNetCore.Mvc;

namespace AeromexicoPrueba.Controllers;

[ApiController]
[Route("api/reservations")]
public sealed class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpPost]
    public IActionResult CreateReservation([FromBody] CreateReservationRequest request)
    {
        try
        {
            var response = _reservationService.CreateReservation(request);
            return Ok(response);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error." });
        }
    }
}
