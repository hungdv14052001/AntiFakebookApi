namespace AntiFakebookApi.Request
{
    public class SetBlockRequest
    {
        public int UserId { get; set; }
        public int Type { get; set; } // 0: block, 1: unblock
    }
}
