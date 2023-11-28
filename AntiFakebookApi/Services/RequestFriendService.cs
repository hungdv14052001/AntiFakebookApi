using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Repositories;
using AntiFakebookApi.Models;
using AntiFakebookApi.Dto;
using AntiFakebookApi.Request;
using AntiFakebookApi.Common.Enum;
using Microsoft.Extensions.Hosting;

namespace AntiFakebookApi.Services
{
    public class RequestFriendService
    {
        private readonly RequestFriendRepository _requestFriendRepository;
        private readonly FriendRepository _friendRepository;
        private readonly AccountRepository _accountRepository;
        private readonly NotificationService _notificationService;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public RequestFriendService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _notificationService = new NotificationService(apiOption, databaseContext, mapper, webHost);
            _requestFriendRepository = new RequestFriendRepository(apiOption, databaseContext, mapper);
            _friendRepository = new FriendRepository(apiOption, databaseContext, mapper);
            _accountRepository = new AccountRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object SetRequestFriend(int accountId, int userId)
        {
            try
            {
                var requestFriend = _requestFriendRepository.FindByCondition(row => (row.AccountIdSendRequest == accountId && row.AccountIdReceive == userId) || (row.AccountIdSendRequest == userId && row.AccountIdReceive == accountId)).FirstOrDefault();
                if (requestFriend != null && accountId == userId)
                {
                    throw new Exception("Can not add request");
                }
                var newRequestFriend = new RequestFriend()
                {
                    AccountIdSendRequest = accountId,
                    AccountIdReceive = userId,
                    Status = 1
                };
                _requestFriendRepository.Create(newRequestFriend);
                _requestFriendRepository.SaveChange();

                // create notification
                _notificationService.CreateNotification(NotificationTypeEnum.SendRequest, userId, accountId, 0);

                return _requestFriendRepository.FindByCondition(row => row.Status == 1 && row.AccountIdSendRequest == accountId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetRequestFriends(int accountId)
        {
            try
            {
                var query = _requestFriendRepository.FindByCondition(row => row.Status == 1 && row.AccountIdReceive == accountId);
                var requestFriendList = query.ToList();
                var userIdList = requestFriendList.Select(row => row.AccountIdSendRequest).ToList();
                var userList = _accountRepository.FindByCondition(row => userIdList.Contains(row.Id)).ToList();
                var requestFriendDtoList = userList.Select(row => _mapper.Map<FriendDto>(row)).ToList();

                foreach (var friendDto in requestFriendDtoList)
                {
                    var count = _friendRepository.FindByCondition(row => (row.AccountIdSend == friendDto.Id && userIdList.Contains(row.AccountIdSend)) || (row.AccountIdReceive == friendDto.Id && userIdList.Contains(row.AccountIdReceive))).Count();
                    friendDto.SameFriends = count;
                }

                return new
                {
                    request = requestFriendDtoList,
                    total = query.Count()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SetAcceptFriend(int accountId, SetAcceptFriendRequest request)
        {
            try
            {
                var reuqestFriend = _requestFriendRepository.FindByCondition(row => row.AccountIdReceive == accountId && row.AccountIdSendRequest == request.UserId && row.Status == 1).FirstOrDefault();
                if (reuqestFriend == null)
                {
                    throw new Exception("reuqestFriend dont exist");
                }
                if (request.IsAccept)
                {
                    reuqestFriend.Status = 2;
                }
                else
                {
                    reuqestFriend.Status = 0;
                }
                reuqestFriend.UpdatedDate = DateTime.Now;
                _requestFriendRepository.UpdateByEntity(reuqestFriend);
                _requestFriendRepository.SaveChange();

                // add friend
                var friend = new Friend()
                {
                    AccountIdSend = request.UserId,
                    AccountIdReceive = accountId
                };
                _friendRepository.Create(friend);
                _friendRepository.SaveChange();

                // create notification
                _notificationService.CreateNotification(NotificationTypeEnum.AcceptFriend, friend.AccountIdSend, accountId, 0);

                return reuqestFriend;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
