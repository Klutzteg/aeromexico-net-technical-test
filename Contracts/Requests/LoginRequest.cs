using System.ComponentModel.DataAnnotations;

namespace AeromexicoPrueba.Contracts.Requests;

public sealed class LoginRequest
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
