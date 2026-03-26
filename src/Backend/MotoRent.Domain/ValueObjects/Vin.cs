namespace MotoRent.Domain.ValueObjects;

public sealed record Vin(string Value) : AlphanumericIdentifier(Value);
