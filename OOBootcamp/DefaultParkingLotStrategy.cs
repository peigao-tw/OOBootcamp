using OOBootcamp;

public class DefaultParkingLotStrategy : IParkingLotStrategy
{
    public ParkingLot? GetAvailableParkingLot(IEnumerable<ParkingLot> parkingLots)
    {
        return parkingLots.FirstOrDefault(p => p.AvailableCount > 0);
    }
}