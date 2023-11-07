namespace AntiFakebookApi.Request
{
    public class CreateIngredientRequest
    {
        public string Name { get; set; }
        public int Unit { get; set; }
        public int Amount { get; set; }
    }
}