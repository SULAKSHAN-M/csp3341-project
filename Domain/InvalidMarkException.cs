namespace StudentRecordManagement.Web.Domain;

public class InvalidMarkException : ArgumentException
{
    public InvalidMarkException(int mark)
        : base($"Mark must be between 0 and 100 (received {mark}).") { }
}
