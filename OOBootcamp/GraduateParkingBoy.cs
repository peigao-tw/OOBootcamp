namespace OOBootcamp;

public class GraduateParkingBoy: ParkingBoy
{
    public GraduateParkingBoy(IEnumerable<ParkingLot> parkingLots): base(parkingLots)
    {
    }

    public override ParkingLot? GetAvailableParkingLot()
    {
        return _parkingLots.FirstOrDefault(p => p.AvailableCount > 0);
    }
}
