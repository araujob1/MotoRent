namespace MotoRent.Domain.ValueObjects;

public record Vin(string Value) : AlphanumericIdentifier(Value);
