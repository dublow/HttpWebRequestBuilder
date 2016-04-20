using System.Collections.Generic;

namespace HttpWebRequestBuilder.Core
{
    public enum ContentType
    {
        Xml,
        Soap,
        Json
    }

    public static class ContentTypeExtension
    {
        public static string ToReal(this ContentType contentType)
        {
            switch (contentType)
            {
                case ContentType.Xml:
                    return "text/xml; charset=utf-8";
                case ContentType.Soap:
                    return "application/soap+xml; charset=UTF-8";
                case ContentType.Json:
                    return "application/json; charset=UTF-8";
                default:
                    return string.Empty;
            }
        }
    }
}
