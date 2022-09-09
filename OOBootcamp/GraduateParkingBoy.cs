using System.Collections.Concurrent;

namespace OOBootcamp;

public class GraduateParkingBoy
{
    // Write your logic here

    private readonly IEnumerable<ParkingLot> _parkingLots;
    private ConcurrentDictionary<Vehicle, ParkingLot> _parkingRecord = new ConcurrentDictionary<Vehicle, ParkingLot>();
    private IParkingLotStrategy _parkingLotStrategy = new DefaultParkingLotStrategy();

    public GraduateParkingBoy(IEnumerable<ParkingLot> parkingLots)
    {
        this._parkingLots = parkingLots;
    }

    public void SetStrategy(IParkingLotStrategy parkingStrategy)
    {
        _parkingLotStrategy = parkingStrategy;
    }

    public bool ParkVehicle(Vehicle vehicle)
    {
        var parkingLot = _parkingLotStrategy.GetAvailableParkingLot(_parkingLots);

        if (parkingLot is null)
            throw new NoParkingSlotAvailableException("No ParkingLot Available");

        bool isParked = parkingLot.ParkVehicle(vehicle);

        if (isParked)
            isParked = _parkingRecord.TryAdd(vehicle, parkingLot);

        return isParked;
    }

    public bool RetrieveVehicle(Vehicle vehicle)
    {
        var parkingLot = _parkingRecord.GetValueOrDefault(vehicle);

        if (parkingLot is null)
            throw new NoParkingSlotRecordException("No ParkingLot Record");

        bool isRemoved = true;

        try
        {
            parkingLot.RetrieveVehicle(vehicle);
            isRemoved = _parkingRecord.TryRemove(vehicle, out parkingLot);
        }
        catch (VehicleNotFoundException e)
        {
            throw e;
        }

        return isRemoved;
    }
}
