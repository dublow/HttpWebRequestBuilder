using System;

namespace HttpWebRequestBuilder.Core
{
    public class DetailledResponse
    {
        public readonly DateTime Start;
        public readonly DateTime End;
        public readonly TimeSpan ElapsedTime;
        public readonly string Response;

        public DetailledResponse(DateTime start, DateTime end, TimeSpan elapsedTime, string response)
        {
            Start = start;
            End = end;
            ElapsedTime = elapsedTime;
            Response = response;
        }
    }
}