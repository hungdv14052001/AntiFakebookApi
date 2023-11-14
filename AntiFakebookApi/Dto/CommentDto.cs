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
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public PosterDto Poster { get; set; }

        public CommentWithPosterDto(Comment comment, PosterDto posterDto)
        {
            Poster = posterDto;
            Id = comment.Id;
            Content = comment.Content;
            CreatedDate = comment.CreatedDate;
        }
    }
}
