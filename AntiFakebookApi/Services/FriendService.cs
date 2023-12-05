using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Repositories;
using AntiFakebookApi.Models;
using AntiFakebookApi.Dto;
using AntiFakebookApi.Request;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AntiFakebookApi.Services
{
    public class FriendService
    {
        private readonly RequestFriendRepository _requestFriendRepository;
        private readonly FriendRepository _friendRepository;
        private readonly AccountRepository _accountRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public FriendService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _requestFriendRepository = new RequestFriendRepository(apiOption, databaseContext, mapper);
            _friendRepository = new FriendRepository(apiOption, databaseContext, mapper);
            _accountRepository = new AccountRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object GetUserFriends(int accountId, int userId)
        {
            try
            {  
                // friend of user in parram
                var friendOfUserList = _friendRepository.FindByCondition(row => row.AccountIdReceive == userId || row.AccountIdSend == userId).ToList();
                var userIdOfUserList = new List<int>();
                foreach (var friend in friendOfUserList)
                {
                    if(friend.AccountIdSend == accountId)
                    {
                        userIdOfUserList.Add(friend.AccountIdReceive);
                    }
                    else
                    {
                        userIdOfUserList.Add(friend.AccountIdSend);
                    }
                }

                // get friend of account is logining
                var friendOfAccountList = _friendRepository.FindByCondition(row => row.AccountIdReceive == accountId || row.AccountIdSend == accountId).ToList();
                var userIdOfAccountList = new List<int>();
                foreach (var friend in friendOfAccountList)
                {
                    if (friend.AccountIdSend == accountId)
                    {
                        userIdOfAccountList.Add(friend.AccountIdReceive);
                    }
                    else
                    {
                        userIdOfAccountList.Add(friend.AccountIdSend);
                    }
                }

                var userList = _accountRepository.FindByCondition(row => userIdOfUserList.Contains(row.Id)).ToList();
                var friendDtoList = userList.Select(row => _mapper.Map<FriendDto>(row)).ToList();
                foreach (var friendDto in friendDtoList)
                {
                    var count = _friendRepository.FindByCondition(row => (row.AccountIdSend == friendDto.Id && userIdOfAccountList.Contains(row.AccountIdSend)) || (row.AccountIdReceive == friendDto.Id && userIdOfAccountList.Contains(row.AccountIdReceive))).Count();
                    friendDto.SameFriends = count;
                }

                return new
                {
                    request = friendDtoList.Select(row=> row.getString()).ToList(),
                    total = friendDtoList.Count().ToString()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetListSuggestedFriends(int accountId)
        {
            try
            {
                // get friend of account is logining
                var friendOfAccountList = _friendRepository.FindByCondition(row => row.AccountIdReceive == accountId || row.AccountIdSend == accountId).ToList();
                var userIdOfAccountList = new List<int>();
                foreach (var friend in friendOfAccountList)
                {
                    if (friend.AccountIdSend == accountId)
                    {
                        userIdOfAccountList.Add(friend.AccountIdReceive);
                    }
                    else
                    {
                        userIdOfAccountList.Add(friend.AccountIdSend);
                    }
                }

                var userList = _accountRepository.FindByCondition(row => !userIdOfAccountList.Contains(row.Id)).ToList();
                var friendDtoList = userList.Select(row => _mapper.Map<FriendDto>(row)).ToList();
                foreach (var friendDto in friendDtoList)
                {
                    var count = _friendRepository.FindByCondition(row => (row.AccountIdSend == friendDto.Id && userIdOfAccountList.Contains(row.AccountIdSend)) || (row.AccountIdReceive == friendDto.Id && userIdOfAccountList.Contains(row.AccountIdReceive))).Count();
                    friendDto.SameFriends = count;
                }

                return new
                {
                    request = friendDtoList.OrderByDescending(row => row.SameFriends).Where(row => row.Id != accountId).Select(row => row.getString()).ToList(),
                    total = friendDtoList.Count().ToString()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
