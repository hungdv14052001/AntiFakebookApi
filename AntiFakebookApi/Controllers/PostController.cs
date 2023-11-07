using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Dto;
using AntiFakebookApi.Request;
using AntiFakebookApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AntiFakebookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : BaseApiController<PostController>
    {
        private readonly PostService _postService;
        public PostController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _postService = new PostService(apiConfig, databaseContext, mapper, webHost);
        }

        /// <summary>
        /// get profile
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("add_post")]
        public MessageData AddPost(AddPostRequest request)
        {
            try
            {
                var res = _postService.AddPost(request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
