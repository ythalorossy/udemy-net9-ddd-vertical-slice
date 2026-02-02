using System.Net;

namespace Blocks.Exceptions;

public class BadResquestException : HttpException
{
    public BadResquestException(string message) : base(HttpStatusCode.BadRequest, message) { }

    public BadResquestException(string message, Exception exception) : base(HttpStatusCode.BadRequest, message, exception) { }
}