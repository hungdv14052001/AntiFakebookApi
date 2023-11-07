namespace AntiFakebookApi.Request
{
    public class CreateCategoryRequest
    {
        public int ParentId { get; set; }
        public string Name { get; set; }
    }
}
