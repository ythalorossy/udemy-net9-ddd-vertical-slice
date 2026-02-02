using Microsoft.AspNetCore.Http;

namespace Blocks.AspNetCore;

public static class Extensions
{
    public static string? BaseUrl(this HttpRequest request)
    {
        if (request == null)
        {
            return null;
        }

        var uriBuilder = new UriBuilder(request.Scheme, request.Host.Host, request.Host.Port ?? -1);

        if (uriBuilder.Uri.IsDefaultPort)
        {
            uriBuilder.Port = -1;
        }

        return uriBuilder.Uri.AbsoluteUri;
    }
}