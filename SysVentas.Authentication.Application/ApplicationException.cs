using System.Runtime.Serialization;
namespace SysVentas.Authentication.Application;
[Serializable]
public class ApplicationException : Exception
{
    public ApplicationException() { }
    public ApplicationException(string message) : base(message) { }
    public ApplicationException(string message, Exception inner) : base(message, inner) { }
    protected ApplicationException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}
