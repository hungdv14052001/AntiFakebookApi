using AntiFakebookApi.Models;

namespace AntiFakebookApi.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class CommentWithPosterDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public object Poster { get; set; }

        public CommentWithPosterDto(Comment comment, PosterDto posterDto)
        {
            Poster = posterDto.GetString();
            Id = comment.Id.ToString();
            Content = comment.Content;
            CreatedDate = comment.CreatedDate;
        }
    }
}
