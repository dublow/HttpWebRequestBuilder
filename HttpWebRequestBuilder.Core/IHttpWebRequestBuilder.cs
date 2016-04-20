using NLog;
using System.Collections.Generic;
using System.Net;

namespace HttpWebRequestBuilder.Core
{
    public interface IHttpWebRequestBuilder
    {
        HttpWebRequest HttpWebRequest { get; }
        IHttpWebRequestBuilder WithCredentials(string login, string password);
        IHttpWebRequestBuilder WithHeaders(Dictionary<string, string> headers);
        IHttpWebRequestBuilder WithHeaders(string key, string value);
        IHttpWebRequestBuilder WithNLogInfo(string loggerName);
        IHttpWebRequestBuilder DisableProxy();
        DetailledResponse DetailledResponse();
        string Response();
    }

    public interface IHttpPostWebRequestBuilder
    {
        IHttpWebRequestBuilder WithBody(string data, ContentType contentType);
    }
}
