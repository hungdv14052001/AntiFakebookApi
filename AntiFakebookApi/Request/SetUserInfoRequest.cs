namespace AntiFakebookApi.Request
{
    public class SetUserInfoRequest
    {
        public string username { get; set; }
        public string description { get; set; }
        public IFormFile? avatar { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public IFormFile? cover_image { get; set; }
        public string Link { get; set; }
    }
}
