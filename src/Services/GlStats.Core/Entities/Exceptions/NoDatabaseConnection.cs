namespace GlStats.Core.Entities.Exceptions;

public class NoDatabaseConnection : DomainException
{
    public NoDatabaseConnection() { }
    public NoDatabaseConnection(string message) : base(message) { }
}