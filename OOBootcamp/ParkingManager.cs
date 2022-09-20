namespace OOBootcamp;

public class ParkingManager : ParkingBoy
{
    public IEnumerable<ParkingBoy> ParkingBoys { get; set; } = new List<ParkingBoy>();

    private ParkingBoy? GetParkingBoy(Vehicle vehicle)
    {
        var parkingBoy = ParkingBoys.Where(boy => boy.Certificate(vehicle))
            .OrderByDescending(boy => boy.Priority);

        return parkingBoy.FirstOrDefault();
    }

    public ParkingManager(IEnumerable<ParkingLot> parkingLots) : base(parkingLots)
    {
    }

    public ParkingManager(IEnumerable<ParkingLot> parkingLots, IEnumerable<ParkingBoy> parkingBoys) :
        base(parkingLots)
    {
        ParkingBoys = parkingBoys;
    }

    public override bool ParkVehicle(Vehicle vehicle)
    {
        // Get Parking Boy
        var parkingBoy = GetParkingBoy(vehicle);

        if (parkingBoy != null)
            return parkingBoy.ParkVehicle(vehicle);

        return base.ParkVehicle(vehicle);
    }

    public override bool Certificate(Vehicle vehicle)
    {
        return true;
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

    // public bool RetrieveVehicle(Vehicle vehicle)
    // {
    // }
}