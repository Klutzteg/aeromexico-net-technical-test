namespace AeromexicoPrueba.Contracts.Responses;

public sealed class ReservationResponse
{
    public int Id { get; set; }
    public List<int> Flights { get; set; } = [];
    public List<int> Passengers { get; set; } = [];
    public string CreatedAt { get; set; } = string.Empty;
}
