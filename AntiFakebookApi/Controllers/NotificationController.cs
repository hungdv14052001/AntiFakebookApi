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
    public class NotificationController : BaseApiController<NotificationController>
    {
        private readonly NotificationService _notificationService;
        public NotificationController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _notificationService = new NotificationService(apiConfig, databaseContext, mapper, webHost);
        }

        [HttpGet]
        [Route("get_notification")]
        public MessageData GetNotification(int? index, int? count)
        {
            try
            {
                var res = _notificationService.GetNotification(AccountId);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
