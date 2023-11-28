namespace AntiFakebookApi.Models
{
    public class KeySearch : BaseModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string KeyWord { get; set; }
        object getString()
        {
            return new
            {
                Id = Id.ToString(),
                AccountId = AccountId.ToString(),
                KeyWord = KeyWord.ToString(),
            };
        }
    }
}
