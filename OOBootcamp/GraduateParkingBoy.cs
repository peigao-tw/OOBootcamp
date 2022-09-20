namespace OOBootcamp;

public class GraduateParkingBoy: ParkingBoy
{
    public GraduateParkingBoy(IEnumerable<ParkingLot> parkingLots): base(parkingLots)
    {
    }

    public override int Priority { get; set; } = 3;

    public override bool Certificate(Vehicle vehicle)
    {
        return vehicle.VehicleType == VehicleType.General ;
    }

    public override ParkingLot? GetAvailableParkingLot()
    {
        return _parkingLots.FirstOrDefault(p => p.AvailableCount > 0);
    }
}
