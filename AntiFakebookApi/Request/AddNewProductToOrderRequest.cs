namespace AntiFakebookApi.Request
{
    public class AddNewProductToOrderRequest
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }
    }
}
