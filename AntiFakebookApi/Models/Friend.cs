namespace AntiFakebookApi.Models
{
    public class Friend : BaseModel
    {
        public int Id { get; set; }
        public int AccountIdSend { get; set; }
        public int AccountIdReceive { get; set; }
    }
}
