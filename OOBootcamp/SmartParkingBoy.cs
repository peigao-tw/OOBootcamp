namespace OOBootcamp;

public class SmartParkingBoy: ParkingBoy
{
    public SmartParkingBoy(IEnumerable<ParkingLot> parkingLots): base(parkingLots)
    {
    }

    public override int Priority { get; set; } = 2;

    public override bool Certificate(Vehicle vehicle)
    {
        return vehicle.VehicleType == VehicleType.General ;
    }

    public override ParkingLot? GetAvailableParkingLot()
    {
        var parkingLot = _parkingLots.OrderByDescending(p => p.AvailableCount)
            .ThenByDescending(p => (double)p.AvailableCount / p.MaxCapacity)
            .FirstOrDefault();

        if (parkingLot is null || parkingLot.AvailableCount == 0)
            return null;

        return parkingLot;
    }
}
