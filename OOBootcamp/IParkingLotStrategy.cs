using OOBootcamp;

public interface IParkingLotStrategy
{
    ParkingLot? GetAvailableParkingLot(IEnumerable<ParkingLot> parkingLots);
}