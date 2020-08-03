using SearchFight.Models;
using System.Collections.Generic;

namespace SearchFight.IServices
{
    public interface ISearchEnginesServices
    {
        SearchFightResponse SearchFight(List<string> queries);
    }
}
