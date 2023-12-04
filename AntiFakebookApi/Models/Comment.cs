namespace AntiFakebookApi.Models
{
    public class Comment : BaseModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }

        object getString()
        {
            return new
            {
                Id = Id.ToString(),
                AccountId = AccountId.ToString(),
                PostId = PostId.ToString(),
                Content = Content.ToString(),
                Status = Status.ToString(),
                Type = Type.ToString(),
                CreatedDate = CreatedDate,
                UpdatedDate = (this.UpdatedDate == null ? "null" : UpdatedDate.ToString())
            };
        }
    }
}
