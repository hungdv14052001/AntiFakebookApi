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
    public class AccountController : BaseApiController<AccountController>
    {
        private readonly AccountService _accountService;
        public AccountController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _accountService = new AccountService(apiConfig, databaseContext, mapper, webHost);
        }

        [HttpGet]
        [Route("get_user_info")]
        public MessageData GetUserInfo(int userId)
        {
            try
            {
                var res = _accountService.GetUserInfo(AccountId, userId);
                return new MessageData(res);
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPut]
        [Route("change_info_after_signup")]
        public MessageData ChangeInfoAfterSignup([FromForm] ChangeInfoAfterSignupRequest request)
        {
            try
            {
                var res = _accountService.UpdateAccount(AccountId ,request);
                return new MessageData(res);
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPost]
        [Route("set_user_info")]
        public MessageData SetUserInfo([FromForm] SetUserInfoRequest request)
        {
            try
            {
                var res = _accountService.SetUserInfo(AccountId, request);
                return new MessageData(res);
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("get_list_blocks")]
        public MessageData GetListBlocks(int? index, int? count)
        {
            try
            {
                var res = _accountService.GetListBlocks(AccountId);
                return new MessageData(res);
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPost]
        [Route("set_block")]
        public MessageData SetBlock(SetBlockRequest request)
        {
            try
            {
                var res = _accountService.SetBlock(AccountId, request);
                return new MessageData(res);
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
