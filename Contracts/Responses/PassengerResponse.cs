namespace AeromexicoPrueba.Contracts.Responses;

public sealed class PassengerResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
