using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Repositories;
using AntiFakebookApi.Request;

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

        public object UpdateAccount(int accountId ,ChangeInfoAfterSignupRequest request)
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

                return account;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
