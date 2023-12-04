namespace AntiFakebookApi.Models
{
    public class KeySearch : BaseModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string KeyWord { get; set; }
        
        public object GetString()
        {
            return new
            {
                Id = Id.ToString(),
                AccountId = AccountId.ToString(),
                KeyWord = KeyWord.ToString(),
                CreatedDate = CreatedDate,
                UpdatedDate = (this.UpdatedDate == null ? "null" : UpdatedDate.ToString())
            };
        }
    }
}
