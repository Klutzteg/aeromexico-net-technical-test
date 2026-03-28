using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AeromexicoPrueba.Validation;

public sealed class ExactDateFormatAttribute : ValidationAttribute
{
    private readonly string _format;

    public ExactDateFormatAttribute(string format)
    {
        _format = format;
        ErrorMessage = $"The field {{0}} must have format {_format}.";
    }

    public override bool IsValid(object? value)
    {
        if (value is not string text || string.IsNullOrWhiteSpace(text))
        {
            return false;
        }

        return DateTime.TryParseExact(text, _format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
