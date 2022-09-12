namespace OOBootcamp;

public class SmartParkingBoy: ParkingBoy
{
    public SmartParkingBoy(IEnumerable<ParkingLot> parkingLots): base(parkingLots)
    {
    }

    public override ParkingLot? GetAvailableParkingLot()
    {
        var parkingLot = _parkingLots.OrderByDescending(p => p.AvailableCount)
            .OrderByDescending(p => (double)p.AvailableCount / p.MaxCapacity)
            .FirstOrDefault();

        if (parkingLot is null || parkingLot.AvailableCount == 0)
            return null;

        return parkingLot;
    }
}
