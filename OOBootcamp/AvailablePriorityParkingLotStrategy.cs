using OOBootcamp;

public class AvailableParkingLotStrategy : IParkingLotStrategy
{
    public ParkingLot? GetAvailableParkingLot(IEnumerable<ParkingLot> parkingLots)
    {

        var parkingLot = parkingLots.OrderByDescending(p => p.AvailableCount).FirstOrDefault();

        if (parkingLot is null || parkingLot.AvailableCount == 0)
            return null;

        return parkingLot;
    }
}