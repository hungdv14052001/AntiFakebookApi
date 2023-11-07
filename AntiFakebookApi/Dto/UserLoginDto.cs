using AntiFakebookApi.Models;

namespace AntiFakebookApi.Dto
{
    public class UserLoginDto
    {
        public string token { get; set; }
        public Account user { get; set; }
    }
}
