using System.Linq;
using System.Net;
using System.Text;

namespace HttpWebRequestBuilder.Core
{
    public class HttpPostWebRequestBuilder : IHttpPostWebRequestBuilder
    {
        private readonly HttpWebRequest _httpWebRequest;
        private readonly HttpWebRequestBuilder _httpWebRequestBuilder;

        public HttpPostWebRequestBuilder(HttpWebRequestBuilder httpWebRequestBuilder, HttpWebRequest httpWebRequest)
        {
            _httpWebRequest = httpWebRequest;
            _httpWebRequestBuilder = httpWebRequestBuilder;
        }

        public IHttpWebRequestBuilder WithBody(string data, ContentType contentType)
        {
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var dataLength = dataBytes.Count();

            _httpWebRequest.ContentLength = dataLength;
            _httpWebRequest.ContentType = contentType.ToReal();

            var stream = _httpWebRequest.GetRequestStream();
            stream.Write(dataBytes, 0, dataBytes.Count());

            return _httpWebRequestBuilder;
        }
    }
}
