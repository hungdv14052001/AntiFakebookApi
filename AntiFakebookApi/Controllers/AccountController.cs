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

        ///// <summary>
        ///// get profile
        ///// </summary>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("UpdateProfile")]
        //public MessageData UpdateProfile(UpdateProfileRequest request)
        //{
        //    try
        //    {
        //        var res = _accountService.UpdateProfile(UserId, request);
        //        return new MessageData { Data = res, Status = 1 };
        //    }
        //    catch (Exception ex)
        //    {
        //        return NG(ex);
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("TestNotify")]
        //public async Task<MessageData> TestNotifyAsync()
        //{
        //    try
        //    {
        //        var connectionList = _connectionManager.GetAllSockets();
        //        foreach (var item in connectionList)
        //        {
        //            var socket = item.Value;
        //            if (socket.State == WebSocketState.Open)
        //            {
        //                var message = "{\"name\": \"Test abc\", \"type\": \"Notify\"}";
        //                await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
        //                                                                  offset: 0,
        //                                                                  count: message.Length),
        //                                   messageType: WebSocketMessageType.Text,
        //                                   endOfMessage: true,
        //                                   cancellationToken: CancellationToken.None);
        //            }
        //        }
        //        return new MessageData { Data = 1, Status = 1 };
        //    }
        //    catch (Exception ex)
        //    {
        //        return NG(ex);
        //    }
        //}
    }
}
