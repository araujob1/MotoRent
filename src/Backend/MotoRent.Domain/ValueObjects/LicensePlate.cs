namespace MotoRent.Domain.ValueObjects;

public sealed record LicensePlate(string Value) : AlphanumericIdentifier(Value);
