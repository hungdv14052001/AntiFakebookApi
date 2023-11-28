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
    }
}
