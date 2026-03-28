namespace AeromexicoPrueba.Contracts.Responses;

public sealed class LoginResponse
{
    public bool Authorized { get; set; }
    public string Message { get; set; } = string.Empty;
}
