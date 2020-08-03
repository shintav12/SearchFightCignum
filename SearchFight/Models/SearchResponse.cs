namespace SearchFight.Models
{
    public class SearchResponse
    {
        public string Query { get; set; }
        public long GoogleTotalResults { get; set; }
        public long BingTotalResults { get; set; }
    }
}
