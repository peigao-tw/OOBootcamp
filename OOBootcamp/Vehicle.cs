namespace OOBootcamp;

public record Vehicle(string LicensePlate)
{
    public readonly string LicensePlate = LicensePlate;

    public VehicleType VehicleType { get; set; } = VehicleType.General;
}