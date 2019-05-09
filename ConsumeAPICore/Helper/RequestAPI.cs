using System;
using System.Net.Http;

namespace ConsumeAPICore.Helper
{
    public class RequestAPI
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8080/");
            return Client;
        }
    }
}
