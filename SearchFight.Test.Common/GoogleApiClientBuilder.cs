using Moq;
using RestSharp;
using SearchFight.Models;
using SearchFight.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SearchFight.Test.Common
{
    public class GoogleApiClientBuilder
    {
        private readonly Mock<ISearchApiClient<GoogleResponse>> _apiClient;
        public GoogleApiClientBuilder()
        {
            _apiClient = new Mock<ISearchApiClient<GoogleResponse>>();
        }

        public GoogleApiClientBuilder WithSearchReturns200OK()
        {
            _apiClient.Setup(x => x.Search(It.IsAny<string>()))
                .Returns(new RestResponse<GoogleResponse>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = new GoogleResponse
                    {
                        SearchInformation = new SearchInformation { TotalResults = "118" }
                    }
                });
            _apiClient.Setup(x => x.Search(It.Is<string>(y => y == "query test 1")))
                .Returns(new RestResponse<GoogleResponse>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = new GoogleResponse
                    {
                        SearchInformation = new SearchInformation { TotalResults = "992" }
                    }
                });
            return this;
        }

        public ISearchApiClient<GoogleResponse> Build()
        {
            return _apiClient.Object;
        }
    }
}
