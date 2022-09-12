using System.Collections.Concurrent;

namespace OOBootcamp;

public abstract class ParkingBoy
{
    // Write your logic here

    protected readonly IEnumerable<ParkingLot> _parkingLots;
    protected ConcurrentDictionary<Vehicle, ParkingLot> _parkingRecord = new ConcurrentDictionary<Vehicle, ParkingLot>();

    public ParkingBoy(IEnumerable<ParkingLot> parkingLots)
    {
        this._parkingLots = parkingLots;
    }

    public bool ParkVehicle(Vehicle vehicle)
    {
        // choose where to park
        var parkingLot = GetAvailableParkingLot();

        // park
        if (parkingLot is null)
            throw new NoParkingSlotAvailableException("No ParkingLot Available");

        bool isParked = parkingLot.ParkVehicle(vehicle);

        if (isParked)
            isParked = _parkingRecord.TryAdd(vehicle, parkingLot);

        return isParked;
    }

    public abstract ParkingLot? GetAvailableParkingLot();

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

    public ParkingLot? GetParkingLot(Vehicle vehicle)
    {
        return _parkingRecord.GetValueOrDefault(vehicle);
    }

}
