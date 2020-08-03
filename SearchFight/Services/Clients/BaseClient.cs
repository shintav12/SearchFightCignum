using RestSharp;
using SearchFight.Shared;
using System;

namespace SearchFight.Services
{
    public class BaseClient
    {
        protected string Host;
        protected IRestResponse<T> ExecuteGetResponse<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.AddHandler("application/json", () => { return Deserializer.Default; });
            client.BaseUrl = new Uri(Host);
            var response = client.Execute<T>(request);
            return response;
        }
    }
}
