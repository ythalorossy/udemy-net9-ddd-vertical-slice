using System.Net;

namespace Blocks.Exceptions;

public class HttpException : Exception
{
    public HttpException(HttpStatusCode httpStatusCode, string message)
        : base(string.IsNullOrEmpty(message) ? httpStatusCode.ToString() : message)
    {
        this.HttpStatusCode = httpStatusCode;
    }

    public HttpException(HttpStatusCode httpStatusCode, string message, Exception exception)
    : base(string.IsNullOrEmpty(message) ? httpStatusCode.ToString() : message, exception)
    {
        this.HttpStatusCode = httpStatusCode;
    }

    public HttpStatusCode HttpStatusCode { get; }
}
