namespace GlStats.Core.Entities.Exceptions;

public class NoConnectionException : DomainException
{
    public NoConnectionException() { }
    public NoConnectionException(string message) : base(message) { }
}