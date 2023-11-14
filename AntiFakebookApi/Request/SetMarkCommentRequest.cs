namespace AntiFakebookApi.Request
{
    public class SetMarkCommentRequest
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Count { get; set; }
        public int MarkId { get; set; }
        public int Type { get; set; }
    }
}
