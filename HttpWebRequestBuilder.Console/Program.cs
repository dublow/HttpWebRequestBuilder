namespace HttpWebRequestBuilder.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = Core.HttpWebRequestBuilder
                            .Get("http://google.com")
                            .WithHeaders("port", "9852")
                            .WithNLogInfo("test")
                            .DetailledResponse();
                            
        }
    }
}
