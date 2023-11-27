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

        ///// <summary>
        ///// get profile
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("GetProfile")]
        //public MessageData GetProfile()
        //{
        //    try
        //    {
        //        var res = _accountService.GetProfile(UserId);
        //        return new MessageData { Data = res, Status = 1 };
        //    }
        //    catch (Exception ex)
        //    {
        //        return NG(ex);
        //    }
        //}

        [HttpPut]
        [Route("change_info_after_signup")]
        public MessageData ChangeInfoAfterSignup([FromForm] ChangeInfoAfterSignupRequest request)
        {
            try
            {
                var res = _accountService.UpdateAccount(AccountId ,request);
                return new MessageData { Data = res };
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
                return new MessageData { Data = res };
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
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
