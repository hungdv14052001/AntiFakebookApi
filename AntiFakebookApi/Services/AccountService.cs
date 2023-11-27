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
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public AccountService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _accountRepository = new AccountRepository(apiOption, databaseContext, mapper);
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
                    id = accountId,
                    username = account.Name,
                    email = account.Email,
                    created = account.CreatedDate,
                    avatar = account.Avatar,


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
                return _accountRepository.FindByCondition(row => accountIdList.Contains(row.Id)).ToList();
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
                _accountRepository.UpdateByEntity(account);
                _accountRepository.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
