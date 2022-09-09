namespace OOBootcamp;

public class NoParkingSlotAvailableException : Exception
{
    public NoParkingSlotAvailableException(string message)
        : base(message)
    {
    }
}