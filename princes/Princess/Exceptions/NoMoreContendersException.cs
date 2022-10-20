namespace Nsu.Princess.Exceptions;

public class NoMoreContendersException : SystemException
{
    public NoMoreContendersException(string message) : base(message){}
}