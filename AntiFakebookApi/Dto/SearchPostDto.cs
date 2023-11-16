using AntiFakebookApi.Models;

namespace AntiFakebookApi.Dto
{
    public class SearchPostDto
    {
        public int Id { get; set; }
        public string Described { get; set; }
        public string Media { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public string CommentAccountIdList { get; set; }
        public string LikedAccountIdList { get; set; }
        public PosterDto Author { get; set; }

        public SearchPostDto(Post post, PosterDto posterDto)
        {
            Author = posterDto;
            Id = post.Id;
            Described = post.Described;
            Media = post.Media;
            Image = post.Image;
            Video = post.Video;
            CommentAccountIdList = post.CommentAccountIdList;
            LikedAccountIdList = post.LikedAccountIdList;
        }
    }
}
