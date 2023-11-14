using AntiFakebookApi.Models;

namespace AntiFakebookApi.Dto
{
    public class PostWithAuthorDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Described { get; set; }
        public string Media { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public string CommentAccountIdList { get; set; }
        public string LikedAccountIdList { get; set; }
        public DateTime CreatedDate { get; set; }
        public PosterDto Author { get; set; }

        public PostWithAuthorDto(Post post, PosterDto posterDto)
        {
            Author = posterDto;
            Id = post.Id;
            AccountId = post.AccountId;
            Described = post.Described;
            Media = post.Media;
            Image = post.Image;
            Video = post.Video;
            CommentAccountIdList = post.CommentAccountIdList;
            CreatedDate = post.CreatedDate;
            LikedAccountIdList = post.LikedAccountIdList;
        }
    }
}
