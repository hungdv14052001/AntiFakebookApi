using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Repositories;
using AntiFakebookApi.Request;

namespace AntiFakebookApi.Services
{
    public class PostService
    {
        private readonly AccountRepository _accountRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public PostService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _accountRepository = new AccountRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object AddPost(AddPostRequest request)
        {
            try
            {
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
