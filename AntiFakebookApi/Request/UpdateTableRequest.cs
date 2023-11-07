namespace AntiFakebookApi.Request
{
    public class UpdateTableRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public int Status { get; set; } = 1;
    }
}
