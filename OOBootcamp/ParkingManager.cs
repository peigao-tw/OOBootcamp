namespace OOBootcamp;

public class ParkingManager
{
    public IEnumerable<ParkingBoy> ParkingBoys { get; set; } = new List<ParkingBoy>();

    public bool ParkVehicle(Vehicle vehicle)
    {
        var parkingBoy = ParkingBoys.FirstOrDefault(boy => boy.Certificate(vehicle));
        if (parkingBoy is null)
            throw new VehicleNotSupportException(vehicle);

        return parkingBoy.ParkVehicle(vehicle);
    }

    public bool RetrieveVehicle(Vehicle vehicle)
    {
        var parkingBoy = ParkingBoys.FirstOrDefault();
        if (parkingBoy is null)
            throw new ShorthandedException(vehicle);

        return parkingBoy.RetrieveVehicle(vehicle);
    }
}