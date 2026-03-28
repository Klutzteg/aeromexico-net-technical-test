using System.ComponentModel.DataAnnotations;

namespace AeromexicoPrueba.Contracts.Requests;

public sealed class CreatePassengerRequest
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;
}
