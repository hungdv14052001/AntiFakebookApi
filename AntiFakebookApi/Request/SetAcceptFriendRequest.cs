namespace AntiFakebookApi.Request
{
    public class SetAcceptFriendRequest
    {
        public int UserId { get; set; }
        public bool IsAccept { get; set; }
    }
}
