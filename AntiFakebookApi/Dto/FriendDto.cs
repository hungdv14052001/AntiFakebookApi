namespace AntiFakebookApi.Dto
{
    public class FriendDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public int SameFriends { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
