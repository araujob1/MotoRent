using System.Text.RegularExpressions;

namespace MotoRent.Domain.ValueObjects;

public abstract partial record AlphanumericIdentifier
{
    public string Value { get; }

    protected AlphanumericIdentifier(string value)
    {
        Value = NonAlphanumericCharacters().Replace(value.ToUpperInvariant(), "");
    }

    [GeneratedRegex(@"[^A-Z0-9]")]
    private static partial Regex NonAlphanumericCharacters();
}
