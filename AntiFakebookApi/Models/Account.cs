namespace AntiFakebookApi.Models
{
    public class Account : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Avatar { get; set; } = "";
        public string? Token { get; set; }
        public string? Session { get; set; }
        public string? BlockedAccountIdList { get; set; }
        public int Status { get; set; }
    }
}
