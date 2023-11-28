using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Repositories;
using AntiFakebookApi.Models;
using AntiFakebookApi.Common.Enum;

namespace AntiFakebookApi.Services
{
    public class NotificationService
    {
        private readonly NotificationRepository _notificationRepository;
        private readonly List<string> _contentList = new List<string>()
        {
            "have felt on your post",
            "have commented on your post",
            "have new post",
            "send you a request friend",
            "accept your request friend",
        };
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public NotificationService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _notificationRepository = new NotificationRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }
        public object CreateNotification(NotificationTypeEnum notificationTypeEnum, int accountId, int fromAccountId, int postId)
        {
            try
            {
                var notification = _notificationRepository.FindByCondition(row => row.AccountId == accountId && row.PostId == postId && row.Type == notificationTypeEnum).FirstOrDefault();
                if (notification != null)
                {
                    notification.IsRead = false;
                    notification.FromAccountId = fromAccountId;
                    notification.UpdatedDate = DateTime.Now;
                    _notificationRepository.UpdateByEntity(notification);
                    _notificationRepository.SaveChange();
                    return true;
                }

                var newNotification = new Notification()
                {
                    Type = notificationTypeEnum,
                    AccountId = accountId,
                    FromAccountId = fromAccountId,
                    PostId = postId,
                    IsRead = false,
                    Content = _contentList[(int)notificationTypeEnum],
                    UpdatedDate = DateTime.Now
                };
                _notificationRepository.Create(newNotification);
                _notificationRepository.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetNotification(int accountId)
        {
            try
            {
                return _notificationRepository.FindByCondition(row => row.AccountId == accountId).OrderByDescending(row => row.UpdatedDate).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SetReadNotification(int accountId, int notificationId)
        {
            try
            {
                var notification = _notificationRepository.FindByCondition(row => row.Id == notificationId && row.AccountId == accountId).FirstOrDefault();
                if (notification == null)
                {
                    throw new Exception("notification does not exist!");
                }
                notification.IsRead = true;
                notification.UpdatedDate = DateTime.Now;
                _notificationRepository.UpdateByEntity(notification);
                _notificationRepository.SaveChange();
                return new
                {
                    badge = _notificationRepository.FindByCondition(row => row.IsRead == false && row.AccountId == accountId).Count(),
                    LastUpdate = notification.UpdatedDate
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
