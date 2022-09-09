namespace OOBootcamp;

public class NoParkingSlotRecordException : Exception
{
    public NoParkingSlotRecordException(string message)
        : base(message)
    {
    }
}