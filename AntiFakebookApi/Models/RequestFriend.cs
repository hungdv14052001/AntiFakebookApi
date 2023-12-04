namespace AntiFakebookApi.Models
{
    public class RequestFriend : BaseModel
    {
        public int Id { get; set; }
        public int AccountIdSendRequest { get; set; }
        public int AccountIdReceive { get; set; }
        public int Status { get; set; } // 0: reject, 1: request, 2: accept

        public object GetString()
        {
            return new
            {
                Id = Id.ToString(),
                AccountIdSendRequest = AccountIdSendRequest.ToString(),
                AccountIdReceive = AccountIdReceive.ToString(),
                Status = Status.ToString(),
            };
        }
    }
}
