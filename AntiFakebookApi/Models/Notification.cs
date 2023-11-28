using AntiFakebookApi.Common.Enum;

namespace AntiFakebookApi.Models
{
    public class Notification : BaseModel
    {
        public int Id { get; set; }
        public NotificationTypeEnum Type { get; set; }
        public int PostId { get; set; }
        public int AccountId { get; set; }
        public int FromAccountId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }

        object getString()
        {
            return new
            {
                Id = Id.ToString(),
                AccountId = AccountId.ToString(),
                Type = ((int)Type).ToString(),
                PostId = PostId.ToString(),
                Content = Content.ToString(),
                IsRead = IsRead.ToString(),
                FromAccountId = FromAccountId.ToString(),
            };
        }
    }
}
