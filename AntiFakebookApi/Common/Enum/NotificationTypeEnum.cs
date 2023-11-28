using System.ComponentModel;

namespace AntiFakebookApi.Common.Enum
{
    public enum NotificationTypeEnum : byte
    {
        [Description("LikePost")] LikePost = 0,
        [Description("CommentPost")] CommentPost = 1,
        [Description("HaveNewPost")] HaveNewPost = 2,
        [Description("SendRequest")] SendRequest = 3,
        [Description("AcceptFriend")] AcceptFriend = 4,
    }
}
