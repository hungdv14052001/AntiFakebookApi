namespace AntiFakebookApi.Request
{
    public class ChangeInfoAfterSignupRequest
    {
        public string UserName { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
