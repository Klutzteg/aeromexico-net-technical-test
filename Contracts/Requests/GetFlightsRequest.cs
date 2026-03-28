using System.ComponentModel.DataAnnotations;
using AeromexicoPrueba.Validation;

namespace AeromexicoPrueba.Contracts.Requests;

public sealed class GetFlightsRequest
{
    [Required]
    [ExactDateFormat("yyyy/MM/dd HH:mm:ss")]
    public string StartDate { get; set; } = string.Empty;

    [Required]
    [ExactDateFormat("yyyy/MM/dd HH:mm:ss")]
    public string EndDate { get; set; } = string.Empty;
}
