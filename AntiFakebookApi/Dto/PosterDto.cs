using AntiFakebookApi.Models;

namespace AntiFakebookApi.Dto
{
    public class PosterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }

        public object GetString()
        {
            return new
            {
                Id = this.Id.ToString(),
                Name = this.Name.ToString(),
                Avatar = this.Avatar.ToString(),
            };
        }
    }
}
