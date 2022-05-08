namespace GlStats.Core.Entities.Exceptions;

public abstract class DomainException : Exception
{
    public DomainException() { }
    public DomainException(string message) : base(message) { }
}