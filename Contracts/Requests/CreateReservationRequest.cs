using System.ComponentModel.DataAnnotations;

namespace AeromexicoPrueba.Contracts.Requests;

public sealed class CreateReservationRequest
{
    [Required]
    [MinLength(1)]
    public List<int> Flights { get; set; } = [];

    [Required]
    [MinLength(1)]
    public List<int> Passengers { get; set; } = [];
}
