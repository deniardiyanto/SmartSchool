namespace SmartSchool.Application.Common.Exceptions;

public class WhatsAppException : Exception
{
    public WhatsAppException(string message)
        : base(message)
    {
    }

    public WhatsAppException(
        string message,
        Exception innerException)
        : base(message, innerException)
    {
    }
}