namespace AntiFakebookApi.Request
{
    public class CheckVerifyCodeRequest
    {
        public string Email { get; set; }
        public string CodeVerify { get; set; }
    }
}
