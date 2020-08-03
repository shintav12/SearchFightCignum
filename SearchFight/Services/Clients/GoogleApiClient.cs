using RestSharp;
using SearchFight.Models;
using SearchFight.Services.Interfaces;

namespace SearchFight.Services.Clients
{
    public class  GoogleApiClient : BaseClient, ISearchApiClient<GoogleResponse>
    {
        private string _key;
        private string _cx;
        public GoogleApiClient(string host, string key, string cx)
        {
            Host = host;
            _key = key;
            _cx = cx;
        }
        public IRestResponse<GoogleResponse> Search(string query)
        {
            var request = new RestRequest($"?key={_key}&cx={_cx}&q={query}", Method.GET);
            return ExecuteGetResponse<GoogleResponse>(request);
        }
    }
}
