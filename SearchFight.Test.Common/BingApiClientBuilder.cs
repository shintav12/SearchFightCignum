using Moq;
using RestSharp;
using SearchFight.Models;
using SearchFight.Services.Interfaces;
using System.Net;

namespace SearchFight.Test.Common
{
    public class BingApiClientBuilder
    {
        private readonly Mock<ISearchApiClient<BingResponse>> _apiClient;
        public BingApiClientBuilder()
        {
            _apiClient = new Mock<ISearchApiClient<BingResponse>>();
        }

        public BingApiClientBuilder WithSearchReturns200OK()
        {
            _apiClient.Setup(x => x.Search(It.IsAny<string>()))
                .Returns(new RestResponse<BingResponse>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = new BingResponse
                    {
                        WebPages = new WebPages { TotalEstimatedMatches = 40 }
                    }
                });
            _apiClient.Setup(x => x.Search(It.Is<string>(y => y == "query test 2")))
                .Returns(new RestResponse<BingResponse>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = new BingResponse
                    {
                        WebPages = new WebPages { TotalEstimatedMatches = 90 }
                    }
                });
            return this;
        }

        public ISearchApiClient<BingResponse> Build()
        {
            return _apiClient.Object;
        }
    }
}
