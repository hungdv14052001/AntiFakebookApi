using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Repositories;
using AntiFakebookApi.Request;
using System.Linq;

namespace AntiFakebookApi.Services
{
    public class AccountService
    {
        private readonly AccountRepository _accountRepository;
        private readonly FriendRepository _friendRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public AccountService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _accountRepository = new AccountRepository(apiOption, databaseContext, mapper);
            _friendRepository = new FriendRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object UpdateAccount(int accountId, ChangeInfoAfterSignupRequest request)
        {
            try
            {
                var account = _accountRepository.FindByCondition(row => row.Id == accountId).FirstOrDefault();
                if (account == null)
                {
                    throw new Exception("Account doesn't exist!");
                }
                if (request.Avatar != null && request.Avatar.FileName != account.Avatar)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\accounts\\avatars\\" + date + request.Avatar.FileName))
                    {
                        request.Avatar.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    account.Avatar = "accounts/avatars/" + date + request.Avatar.FileName;
                }
                account.Name = request.UserName;
                account.UpdatedDate = DateTime.UtcNow;
                _accountRepository.UpdateByEntity(account);
                _accountRepository.SaveChange();

                return new
                {
                    id = accountId.ToString(),
                    username = account.Name.ToString(),
                    email = account.Email.ToString(),
                    created = account.CreatedDate.ToString(),
                    avatar = account.Avatar.ToString(),


                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SetUserInfo(int accountId, SetUserInfoRequest request)
        {
            try
            {
                var account = _accountRepository.FindByCondition(row => row.Id == accountId).FirstOrDefault();
                if (account == null)
                {
                    throw new Exception("Account doesn't exist!");
                }
                if (request.avatar != null && request.avatar.FileName != account.Avatar)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\accounts\\avatars\\" + date + request.avatar.FileName))
                    {
                        request.avatar.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    account.Avatar = "accounts/avatars/" + date + request.avatar.FileName;
                }
                if (account == null)
                {
                    throw new Exception("Account doesn't exist!");
                }
                if (request.cover_image != null && request.cover_image.FileName != account.CoverImage)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\accounts\\cover\\" + date + request.cover_image.FileName))
                    {
                        request.cover_image.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    account.CoverImage = "accounts/cover/" + date + request.cover_image.FileName;
                }
                account.Description = request.description;
                account.City = request.city;
                account.Country = request.country;
                account.Adress = request.address;
                account.Name = request.username;
                account.UpdatedDate = DateTime.UtcNow;
                _accountRepository.UpdateByEntity(account);
                _accountRepository.SaveChange();

                return new
                {
                    avatar = account.Avatar.ToString(),
                    cover_image = account.CoverImage.ToString(),
                    link = request.Link.ToString(),
                    city = account.City.ToString(),
                    country = account.Country.ToString(),
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetUserInfo(int accountId, int userId)
        {
            try
            {
                var user = _accountRepository.FindOrFail(userId);
                if(user == null)
                {
                    throw new Exception("UserId does not exist");
                }
                var friend = _friendRepository.FindByCondition(row => (row.AccountIdSend == accountId && row.AccountIdReceive == userId) || (row.AccountIdSend == userId && row.AccountIdReceive == accountId)).FirstOrDefault();
                var is_friend = friend != null ? "true" : "false";
                return new
                {
                    id = user.Id.ToString(),
                    username = user.Name,
                    created = user.CreatedDate,
                    description = user.Description,
                    avatar = user.Avatar,
                    address = user.Adress,
                    city = user.City,
                    listing = _friendRepository.FindByCondition(row => row.AccountIdSend == userId || row.AccountIdReceive == userId).Count().ToString(),
                    is_friend = is_friend,
                    coins = user.Coins.ToString()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetListBlocks(int accountId)
        {
            try
            {
                var account = _accountRepository.FindByCondition(row => row.Id == accountId).FirstOrDefault();
                if (account == null)
                {
                    throw new Exception("Account doesn't exist!");
                }
                var accountIdList = new List<int>();
                if (!string.IsNullOrEmpty(account.BlockedAccountIdList))
                {
                    accountIdList = account.BlockedAccountIdList?.Split(',')?.Select(Int32.Parse)?.ToList();
                }
                return _accountRepository.FindByCondition(row => accountIdList.Contains(row.Id)).ToList().Select(row => row.GetString()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SetBlock(int accountId, SetBlockRequest request)
        {
            try
            {
                var account = _accountRepository.FindByCondition(row => row.Id == accountId).FirstOrDefault();
                if (account == null)
                {
                    throw new Exception("Account doesn't exist!");
                }
                var accountBlocked = _accountRepository.FindByCondition(row => row.Id == request.UserId).FirstOrDefault();
                if (account == null)
                {
                    throw new Exception("UserId doesn't exist!");
                }

                var accountIdList = new List<int>();
                if (!string.IsNullOrEmpty(account.BlockedAccountIdList))
                {
                    accountIdList = account.BlockedAccountIdList?.Split(',')?.Select(Int32.Parse)?.ToList();
                }
                if (request.Type == 0)
                {
                    if (accountIdList.Contains(accountBlocked.Id))
                    {
                        throw new Exception("This user has been blocked");
                    }
                    accountIdList.Add(accountBlocked.Id);
                }
                else if (request.Type == 1)
                {
                    if (!accountIdList.Contains(accountBlocked.Id))
                    {
                        throw new Exception("This user has not been blocked");
                    }
                    accountIdList.Remove(accountBlocked.Id);
                }
                var blockedAccountIdList = "";
                for (int i = 0; i < accountIdList.Count() - 1; i++)
                {
                    blockedAccountIdList += accountIdList[i] + ", ";
                }
                if (accountIdList.Count > 0)
                {
                    blockedAccountIdList += accountIdList[accountIdList.Count() - 1];
                }
                account.BlockedAccountIdList = blockedAccountIdList;
                account.UpdatedDate = DateTime.Now;
                _accountRepository.UpdateByEntity(account);
                _accountRepository.SaveChange();
                return "true";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
