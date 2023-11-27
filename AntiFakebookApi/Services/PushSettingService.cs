using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Repositories;
using AntiFakebookApi.Request;
using AntiFakebookApi.Models;
using AntiFakebookApi.Dto;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Security.Principal;

namespace AntiFakebookApi.Services
{
    public class PushSettingService
    {
        private readonly AccountRepository _accountRepository;
        private readonly PushSettingRepository _pushSettingRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public PushSettingService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _accountRepository = new AccountRepository(apiOption, databaseContext, mapper);
            _pushSettingRepository = new PushSettingRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object SetPushSetting(int accountId, SetPushSettingRequest request)
        {
            try
            {
                var pushSetting = _pushSettingRepository.FindByCondition(row => accountId == row.AccountId).FirstOrDefault();
                if (pushSetting == null)
                {
                    throw new Exception("pushSetting doesn't exist!");
                }
                pushSetting.AccountId = accountId;
                pushSetting.LikeComment = request.LikeComment;
                pushSetting.FromFriends = request.FromFriends;
                pushSetting.RequestedFriend = request.RequestedFriend;
                pushSetting.SuggestedFriend = request.SuggestedFriend;
                pushSetting.BirthDay = request.BirthDay;
                pushSetting.Video = request.Video;
                pushSetting.Report = request.Report;
                pushSetting.SoundOn = request.SoundOn;
                pushSetting.NotificationOn = request.NotificationOn;
                pushSetting.VibrandOn = request.VibrandOn;
                pushSetting.LedOn = request.LedOn;
                _pushSettingRepository.UpdateByEntity(pushSetting);
                _pushSettingRepository.SaveChange();
                return pushSetting;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public object GetPushSetting(int accountId)
        {
            try
            {
                var pushSetting = _pushSettingRepository.FindByCondition(row => accountId == row.AccountId).FirstOrDefault();
                return new
                {
                    like_comment = pushSetting.LikeComment,
                    from_friends = pushSetting.FromFriends,
                    requested_friend = pushSetting.RequestedFriend,
                    suggested_friend = pushSetting.SuggestedFriend,
                    birthday = pushSetting.BirthDay,
                    video = pushSetting.Video,
                    report = pushSetting.Report,
                    sound_on = pushSetting.SoundOn,
                    notification_on = pushSetting.NotificationOn,
                    vibrant_on = pushSetting.VibrandOn,
                    led_on = pushSetting.LedOn,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
