namespace Blocks.Domain.ValueObjects;

public class StringValueObject : IEquatable<StringValueObject>
{
    public string Value { get; protected set; } = default!;

    public override string ToString() => Value.ToString();

    public override int GetHashCode() => Value.GetHashCode();

    public bool Equals(StringValueObject? other) => Value.Equals(other?.Value);

    public static implicit operator string(StringValueObject @valueObject) => @valueObject.Value;
}