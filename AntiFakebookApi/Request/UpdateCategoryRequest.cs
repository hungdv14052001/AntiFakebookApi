
namespace AntiFakebookApi.Request
{
    public class UpdateCategoryRequest
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; } = 1;
    }
}
