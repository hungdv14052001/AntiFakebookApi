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
    public class FriendController : BaseApiController<FriendController>
    {
        private readonly FriendService _friendService;
        public FriendController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _friendService = new FriendService(apiConfig, databaseContext, mapper, webHost);
        }

        [HttpGet]
        [Route("get_user_friends")]
        public MessageData GetUserFriends(int userId, int? index, int? count)
        {
            try
            {
                var res = _friendService.GetUserFriends(AccountId, userId);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("get_list_suggested_friends")]
        public MessageData GetListSuggestedFriends(int? index, int? count)
        {
            try
            {
                var res = _friendService.GetListSuggestedFriends(AccountId);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
