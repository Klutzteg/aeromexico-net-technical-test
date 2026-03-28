namespace AeromexicoPrueba.Domain;

public sealed class Reservation
{
    public int Id { get; set; }
    public List<int> FlightIds { get; set; } = [];
    public List<int> PassengerIds { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}
