namespace AntiFakebookApi.Request
{
    public class SaveNewFullOrderRequest
    {
        public CreateNewOrderRequest NewOrder { get; set; }
        public List<AddNewProductToOrderRequest> NewOrderDetailList { get; set; }
    }
}
