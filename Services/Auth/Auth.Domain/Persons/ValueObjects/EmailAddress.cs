using Blocks.Core;
using Blocks.Domain.ValueObjects;
using System.Text.RegularExpressions;

namespace Auth.Domain.Persons.ValueObjects;

public class EmailAddress : StringValueObject
{
    public EmailAddress(string value)
    {
        Value = value;
        NormalizedEmail = value.ToUpperInvariant();
    }

    public string NormalizedEmail { get; internal set; }

    public static EmailAddress Create(string value)
    {
        Guard.ThrowIfNullOrWhiteSpace(value);
        Guard.ThrowIfFalse(IsValidEmail(value), "Invalid email address format.");

        return new EmailAddress(value);
    }

    private static bool IsValidEmail(string value)
    {
        var pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

        return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);
    }

    public static implicit operator EmailAddress(string value)
        => Create(value);

    public static implicit operator string(EmailAddress email)
        => email.Value;

    public override int GetHashCode()
    {
        return NormalizedEmail.GetHashCode();
    }
}
