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
    public class PostHandleController : BaseApiController<PostHandleController>
    {
        private readonly PostHandleService _postHandleService;
        public PostHandleController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _postHandleService = new PostHandleService(apiConfig, databaseContext, mapper, webHost);
        }

        [HttpGet]
        [Route("get_mark_comment")]
        public MessageData GetMarkComment(int id)
        {
            try
            {
                var res = _postHandleService.GetMarkComment(id);
                return new MessageData(res);
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// add post
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("set_mark_comment")]
        public MessageData SetMarkComment(SetMarkCommentRequest request)
        {
            try
            {
                var res = _postHandleService.SetMarkComment(AccountId, request);
                return new MessageData(res);
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPost]
        [Route("feel")]
        public MessageData Feel(FeelRequest request)
        {
            try
            {
                var res = _postHandleService.Feel(AccountId, request);
                return new MessageData(res);
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
