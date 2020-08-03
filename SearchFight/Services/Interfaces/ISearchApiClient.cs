using RestSharp;

namespace SearchFight.Services.Interfaces
{
    public interface ISearchApiClient<T>
    {
        IRestResponse<T> Search(string query);
    }
}
