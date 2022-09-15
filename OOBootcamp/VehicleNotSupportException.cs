namespace OOBootcamp;

public class VehicleNotSupportException : Exception
{
    private Vehicle _missiongVehicle;
    public VehicleNotSupportException(Vehicle vehicle)
    {
        _missiongVehicle = vehicle;
    }
}