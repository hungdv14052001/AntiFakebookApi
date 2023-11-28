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
    public class PushSettingController : BaseApiController<PushSettingController>
    {
        private readonly PushSettingService _pushSettingService;
        public PushSettingController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _pushSettingService = new PushSettingService(apiConfig, databaseContext, mapper, webHost);
        }

        [HttpGet]
        [Route("get_push_settings")]
        public MessageData GetPushSetting()
        {
            try
            {
                var res = _pushSettingService.GetPushSetting(AccountId);
                return new MessageData(res);
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPut]
        [Route("set_push_settings")]
        public MessageData SetPushSetting(SetPushSettingRequest request)
        {
            try
            {
                var res = _pushSettingService.SetPushSetting(AccountId, request);
                return new MessageData(res);
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
