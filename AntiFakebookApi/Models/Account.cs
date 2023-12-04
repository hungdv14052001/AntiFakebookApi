namespace AntiFakebookApi.Models
{
    public class Account : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Avatar { get; set; } = "";
        public string? Token { get; set; } = "";
        public string? Session { get; set; }
        public string BlockedAccountIdList { get; set; } = "";
        public int Coins { get; set; }
        public string Uuid { get; set; } = "";
        public int Status { get; set; }
        public string CodeVerify { get; set; } = "";

        public object GetString()
        {
            return new
            {
                Id = this.Id.ToString(),
                Name = this.Name.ToString(),
                Password = this.Password.ToString(),
                Email = this.Email.ToString(),
                Avatar = this.Avatar.ToString(),
                BlockedAccountIdList = this.BlockedAccountIdList.ToString(),
                Coins = this.Coins.ToString(),
                Uuid = this.Uuid.ToString(),
                Status = this.Status.ToString(),
                CodeVerify = this.CodeVerify.ToString(),
                CreatedDate = CreatedDate,
                UpdatedDate = (this.UpdatedDate == null ? "null" : UpdatedDate.ToString())
            };
        }
    }
}
