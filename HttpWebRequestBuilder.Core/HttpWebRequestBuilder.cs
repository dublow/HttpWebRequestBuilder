using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NLog;

namespace HttpWebRequestBuilder.Core
{
    public class HttpWebRequestBuilder : IHttpWebRequestBuilder
    {
        private readonly HttpWebRequest _httpWebRequest;
        private Logger _logger;

        protected HttpWebRequestBuilder(string uri, HttpMethod httpMethod)
        {
            _httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
            _httpWebRequest.Method = httpMethod.ToString();
        }

        public static IHttpPostWebRequestBuilder Post(string uri)
        {
            var post = new HttpWebRequestBuilder(uri, HttpMethod.Post);
            return new HttpPostWebRequestBuilder(post, post._httpWebRequest);
        }

        public static IHttpWebRequestBuilder Get(string uri)
        {
            return new HttpWebRequestBuilder(uri, HttpMethod.Get);
        }

        public HttpWebRequest HttpWebRequest
        {
            get
            {
                return _httpWebRequest;
            }
        }

        public IHttpWebRequestBuilder WithCredentials(string login, string password)
        {
            _httpWebRequest.Credentials = new NetworkCredential(login, password);
            return this;
        }

        public IHttpWebRequestBuilder WithHeaders(Dictionary<string, string> headers)
        {
            InitHeaders();
            foreach (var header in headers)
                _httpWebRequest.Headers.Add(header.Key, header.Value);
            return this;
        }

        public IHttpWebRequestBuilder WithHeaders(string key, string value)
        {
            InitHeaders();
            _httpWebRequest.Headers.Add(key, value); 
            return this;
        }

        public IHttpWebRequestBuilder WithNLogInfo(string loggerName)
        {
            _logger = LogManager.GetLogger(loggerName);
            return this;
        }

        public IHttpWebRequestBuilder DisableProxy()
        {
            _httpWebRequest.Proxy = new WebProxy();
            return this;
        }

        public string Response()
        {
            return GetDetailledResponse().Response;
        }

        public DetailledResponse DetailledResponse()
        {
            return GetDetailledResponse();
        }

        private DetailledResponse GetDetailledResponse()
        {
            using (var timer = new Timer())
            {
                try
                {
                    var start = DateTime.UtcNow;
                    using (var webResponse = _httpWebRequest.GetResponse())
                    {
                        using (var webStream = new StreamReader(webResponse.GetResponseStream()))
                        {
                            var response = webStream.ReadToEnd();
                            AddLog(response, timer.Elapsed);
                            return new DetailledResponse(start, DateTime.UtcNow, timer.Elapsed, Response());
                        }
                    }
                }
                catch (Exception ex)
                {
                    AddLog(ex.StackTrace, timer.Elapsed);
                    throw;
                }
            }
        }

        private void InitHeaders()
        {
            if (_httpWebRequest.Headers == null)
                _httpWebRequest.Headers = new WebHeaderCollection();
        }

        private void AddLog(string response, TimeSpan elapsed)
        {
            if (_logger == null)
                return;

            response = string.IsNullOrEmpty(response) ? "No Response" : response;
            _logger.Info("{0}|{1}|Uri:{2}|Response:{3}", DateTime.UtcNow, elapsed, _httpWebRequest.RequestUri, response);
        }
    }
}
