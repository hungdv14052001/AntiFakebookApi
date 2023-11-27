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
    public class RequestFriendController : BaseApiController<RequestFriendController>
    {
        private readonly RequestFriendService _requestFriendService;
        public RequestFriendController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _requestFriendService = new RequestFriendService(apiConfig, databaseContext, mapper, webHost);
        }

        [HttpPost]
        [Route("set_request_friend")]
        public MessageData SetRequestFriend(SetRequestFriendRequest request)
        {
            try
            {
                var res = _requestFriendService.SetRequestFriend(AccountId, request.UserId);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("get_requested_friends")]
        public MessageData GetRequestFriends(int? index, int? count)
        {
            try
            {
                var res = _requestFriendService.GetRequestFriends(AccountId);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPost]
        [Route("set_accept_friend")]
        public MessageData SetAcceptFriend(SetAcceptFriendRequest request)
        {
            try
            {
                var res = _requestFriendService.SetAcceptFriend(AccountId, request);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
