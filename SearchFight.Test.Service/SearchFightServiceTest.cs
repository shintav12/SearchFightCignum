using FluentAssertions;
using NUnit.Framework;
using SearchFight.Services;
using SearchFight.Test.Common;
using System.Collections.Generic;
using System.Linq;

namespace SearchFight.Test.Service
{
    [TestFixture]
    public class SearchFightServiceTest
    {
        private SearchEnginesServices _service;
        private GoogleApiClientBuilder _googleSearchEngineApiClientBuilder;
        private BingApiClientBuilder _bingSearchEngineApiClientBuilder;

        [SetUp]
        public void Setup()
        {
            _googleSearchEngineApiClientBuilder = new GoogleApiClientBuilder();
            _bingSearchEngineApiClientBuilder = new BingApiClientBuilder();
            _service = new SearchEnginesServices(_googleSearchEngineApiClientBuilder.Build(), _bingSearchEngineApiClientBuilder.Build());
        }

        [Test]
        public void SearchFight_ValidQueryList_ReturnResultWithData()
        {
            //Arrange
            _googleSearchEngineApiClientBuilder.WithSearchReturns200OK();
            _bingSearchEngineApiClientBuilder.WithSearchReturns200OK();

            //Act
            var result = _service.SearchFight(new List<string> { "query test 1", "query test 2" });

            //Assert
            result.QueriesResults.Count.Should().Be(2);
            result.QueriesResults.First().GoogleTotalResults.Should().Be(992);
            result.QueriesResults.First().BingTotalResults.Should().Be(40);
            result.GoogleWinner.Should().Be("query test 1");
            result.BingWinner.Should().Be("query test 2");
            result.TotalWinner.Should().Be("query test 1");
        }

        [Test]
        public void SearchFight_EmptyQueryList_ReturnResultWithEmptyData()
        {
            //Act
            var result = _service.SearchFight(new List<string> { });

            //Assert
            result.QueriesResults.Count.Should().Be(0);
            result.GoogleWinner.Should().BeNull();
            result.BingWinner.Should().BeNull();
            result.TotalWinner.Should().BeNull();
        }
    }
}