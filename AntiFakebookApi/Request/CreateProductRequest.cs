namespace AntiFakebookApi.Request
{
    public class CreateProductRequest
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public IFormFile? Thumbnail { get; set; }
        public int Status { get; set; }
    }
}
