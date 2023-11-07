namespace AntiFakebookApi.Request
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string KeySecret { get; set; }
        public string NewPassword { get; set; }
    }
}
