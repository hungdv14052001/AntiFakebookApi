namespace AntiFakebookApi.Request
{
    public class AddPostRequest
    {
        public IFormFile Image { get; set; }
        public IFormFile Video { get; set; }
        public string Described { get; set; }
        public int Status { get; set; }
    }
}
