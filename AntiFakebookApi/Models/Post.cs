namespace AntiFakebookApi.Models
{
    public class Post : BaseModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Content { get; set; }
        public string Media { get; set; }
        public string CommentAccountIdList { get; set; }
        public string LikedAccountIdList { get; set; }
        public int Status { get; set; }
    }
}
