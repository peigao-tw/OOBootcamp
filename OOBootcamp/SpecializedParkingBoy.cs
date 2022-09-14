namespace OOBootcamp;

public class SpecializedParkingBoy: ParkingBoy
{
    public SpecializedParkingBoy(IEnumerable<ParkingLot> parkingLots): base(parkingLots)
    {
    }

    public override bool Certificate(Vehicle vehicle)
    {
        if (vehicle.VehicleType == VehicleType.General || vehicle.VehicleType == VehicleType.SuperCar || vehicle.VehicleType == VehicleType.Trunk)
            return true;

        return false;
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
