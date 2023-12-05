namespace AntiFakebookApi.Request
{
    public class EditPostRequest
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? Video { get; set; }
        public string Described { get; set; }
        public string Status { get; set; }
        public string auto_accept { get; set; }
    }
}
