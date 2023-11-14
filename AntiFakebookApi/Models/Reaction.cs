namespace AntiFakebookApi.Models
{
    public class Reaction : BaseModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int PostId { get; set; }
        public int Type { get; set; } // 0: dissappointed, 1: kudos
    }
}
