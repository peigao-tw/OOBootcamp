namespace OOBootcamp;

public class ShorthandedException : Exception
{
    private Vehicle _missiongVehicle;
    public ShorthandedException(Vehicle vehicle)
    {
        _missiongVehicle = vehicle;
    }
}