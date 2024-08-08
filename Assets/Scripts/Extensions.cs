using System.Net;

public static class Extensions
{
    public static string ToHeaderString(this HttpRequestHeader source)
    {
        return source switch
        {
            HttpRequestHeader.CacheControl => "Cache-Control",
            HttpRequestHeader.KeepAlive => "Keep-Alive",
            HttpRequestHeader.TransferEncoding => "Transfer-Encoding",
            HttpRequestHeader.ContentLength => "Content-Length",
            HttpRequestHeader.ContentType => "Content-Type",
            HttpRequestHeader.ContentEncoding => "Content-Encoding",
            HttpRequestHeader.ContentLanguage => "Content-Language",
            HttpRequestHeader.ContentLocation => "Content-Location",
            HttpRequestHeader.ContentMd5 => "Content-MD5",
            HttpRequestHeader.ContentRange => "Content-Range",
            HttpRequestHeader.LastModified => "Last-Modified",
            HttpRequestHeader.AcceptCharset => "Accept-Charset",
            HttpRequestHeader.AcceptEncoding => "Accept-Encoding",
            HttpRequestHeader.AcceptLanguage => "Accept-Language",
            HttpRequestHeader.IfMatch => "If-Match",
            HttpRequestHeader.IfModifiedSince => "If-Modified-Since",
            HttpRequestHeader.IfNoneMatch => "If-None-Match",
            HttpRequestHeader.IfRange => "If-Range",
            HttpRequestHeader.IfUnmodifiedSince => "If-Unmodified-Since",
            HttpRequestHeader.MaxForwards => "Max-Forwards",
            HttpRequestHeader.ProxyAuthorization => "Proxy-Authorization",
            HttpRequestHeader.UserAgent => "User-Agent",
            HttpRequestHeader.Accept => "Accept",
            HttpRequestHeader.Allow => "Allow",
            HttpRequestHeader.Authorization => "Authorization",
            HttpRequestHeader.Connection => "Connection",
            HttpRequestHeader.Cookie => "Cookie",
            HttpRequestHeader.Date => "Date",
            HttpRequestHeader.Expect => "Expect",
            HttpRequestHeader.Expires => "Expires",
            HttpRequestHeader.From => "From",
            HttpRequestHeader.Host => "Host",
            HttpRequestHeader.Pragma => "Pragma",
            HttpRequestHeader.Range => "Range",
            HttpRequestHeader.Referer => "Referer",
            HttpRequestHeader.Te => "Te",
            HttpRequestHeader.Trailer => "Trailer",
            HttpRequestHeader.Translate => "Translate",
            HttpRequestHeader.Upgrade => "Upgrade",
            HttpRequestHeader.Via => "Via",
            HttpRequestHeader.Warning => "Warning",
            _ => source.ToString()
        };
    }
}
