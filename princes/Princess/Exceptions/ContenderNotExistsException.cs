namespace Nsu.Princess.Exceptions;

public class ContenderNotExistsException : ArgumentException
{
    public ContenderNotExistsException(string message) : base(message){}
}