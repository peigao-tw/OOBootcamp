namespace OOBootcamp;

public class GraduateParkingBoy
{
    private readonly List<ParkingLot> _parkingLots;
    private readonly Dictionary<Vehicle, int> _vehicleLocation; // value is the index of _parkingLots
    private int _lastParkedIndex = -1;
    
    public GraduateParkingBoy(List<ParkingLot> parkingLots)
    {
        _parkingLots = parkingLots;
        _vehicleLocation = new Dictionary<Vehicle, int>(50);
    }

    public void ParkVehicle(Vehicle vehicle)
    {
        for (int i = _lastParkedIndex + 1; i < _parkingLots.Count; i++)
        {
            var index = i % _parkingLots.Count;
            if (_parkingLots[index].AvailableCount > 0 && _parkingLots[index].ParkVehicle(vehicle))
            {
                _lastParkedIndex = index;
                _vehicleLocation.Add(vehicle, index);
                return;
            }
        }

        throw new NoParkingSlotAvailableException();
    }

    public double RetrieveVehicle(string licensePlate)
    {
        var vehicle = new Vehicle(licensePlate);
        if (_vehicleLocation.ContainsKey(vehicle))
        {
            return _parkingLots[_vehicleLocation[vehicle]].RetrieveVehicle(vehicle);
        }

        throw new VehicleNotFoundException(vehicle);
    }
}