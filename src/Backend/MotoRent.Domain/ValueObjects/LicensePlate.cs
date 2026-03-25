namespace MotoRent.Domain.ValueObjects;

public record LicensePlate(string Value) : AlphanumericIdentifier(Value);
