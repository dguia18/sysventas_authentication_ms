using System.Runtime.Serialization;
namespace SysVentas.Authentication.Application.Base;

[Serializable]
public class SysVentasApplicationException : Exception
{
    //
    // For guidelines regarding the creation of new exception types, see
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    // and
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    //

    public SysVentasApplicationException() { }
    public SysVentasApplicationException(string message) : base(message) { }
    public SysVentasApplicationException(string message, Exception inner) : base(message, inner) { }
    protected SysVentasApplicationException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}