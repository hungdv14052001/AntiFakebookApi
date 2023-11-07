namespace AntiFakebookApi.Models
{
    public class Comment : BaseModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
    }
}
