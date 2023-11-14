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
        /// add post
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add_post")]
        public MessageData AddPost([FromForm]AddPostRequest request)
        {
            try
            {
                var res = _postService.AddPost(AccountId, request);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Get post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_post")]
        public MessageData GetPost(int id)
        {
            try
            {
                var res = _postService.GetPost(id);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Delete post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete_post")]
        public MessageData DeletePost(int id)
        {
            try
            {
                var res = _postService.DeletePost(AccountId, id);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("get_list_posts")]
        public MessageData GetListPost(int? userId, int? inCamPaint)
        {
            try
            {
                var res = _postService.GetListPost(userId, inCamPaint);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
