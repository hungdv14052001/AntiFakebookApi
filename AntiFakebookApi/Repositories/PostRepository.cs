using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Models;
using AntiFakebookApi.Respositories;

namespace AntiFakebookApi.Repositories
{
    public class PostRepository : BaseRespository<Post>
    {
        private IMapper _mapper;
        public PostRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }

    }
}
