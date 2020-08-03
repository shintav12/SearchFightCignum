using RestSharp;
using SearchFight.Models;
using SearchFight.Services.Interfaces;

namespace SearchFight.Services
{
    public class BingApiClient : BaseClient, ISearchApiClient<BingResponse>
    {
        private string _key;
        public BingApiClient(string host, string key)
        {
            Host = host;
            _key = key;
        }
        public IRestResponse<BingResponse> Search(string query)
        {
            var request = new RestRequest($"/search?q={query}", Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", _key);
            return ExecuteGetResponse<BingResponse>(request);
        }
    }
}
