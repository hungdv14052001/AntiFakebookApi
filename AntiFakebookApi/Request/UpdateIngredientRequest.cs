namespace AntiFakebookApi.Request
{
    public class UpdateIngredientRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
        public int Amount { get; set; }
        public int Status { get; set; } = 1;
    }
}
