using OOBootcamp;

public class BalanceParkingLotStrategy : IParkingLotStrategy
{
    public ParkingLot? GetAvailableParkingLot(IEnumerable<ParkingLot> parkingLots)
    {
        var average = parkingLots.Average(p => (double)p.AvailableCount);
        if (0 == average)
            return null;

        return parkingLots.FirstOrDefault(p => p.AvailableCount >= average);
    }
}