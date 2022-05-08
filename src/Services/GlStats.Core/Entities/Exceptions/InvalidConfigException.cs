namespace GlStats.Core.Entities.Exceptions;

public class InvalidConfigException : DomainException
{
    public InvalidConfigException() { }
    public InvalidConfigException(string message) : base(message) { }
}