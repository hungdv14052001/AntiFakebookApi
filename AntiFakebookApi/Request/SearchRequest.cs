namespace AntiFakebookApi.Request
{
    public class SearchRequest
    {
        public string? Keyword { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
