using SearchFight.IServices;
using SearchFight.Models;
using System;
using System.Linq;

namespace SearchFight
{
    public class SearchFight
    {
        private readonly ISearchEnginesServices _service;
        public SearchFight(ISearchEnginesServices service)
        {
            _service = service;
        }
        public void Run(string[] args)
        {
            if (args.Length == 0) Console.ReadLine();
            var searchFightResponse = _service.SearchFight(args.ToList());
            DisplayResults(searchFightResponse);
        }

        private void DisplayResults(SearchFightResponse searchFightResponse)
        {
            foreach (var query in searchFightResponse.QueriesResults)
            {
                Console.WriteLine($"{query.Query}: Google: {query.GoogleTotalResults} Bing: {query.BingTotalResults}");
            }
            Console.WriteLine($"Google Winner: {searchFightResponse.GoogleWinner}");
            Console.WriteLine($"Bing Winner: {searchFightResponse.BingWinner}");
            Console.WriteLine($"Total Winner: {searchFightResponse.TotalWinner}");
        }
    }
}
