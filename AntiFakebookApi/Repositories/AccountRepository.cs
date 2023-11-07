using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Models;
using AntiFakebookApi.Request;
using AntiFakebookApi.Respositories;
using AntiFakebookApi.Utility;

namespace AntiFakebookApi.Repositories
{
    public class AccountRepository : BaseRespository<Account>
    {
        private IMapper _mapper;
        public AccountRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }

        /// <summary>
        /// UserLogin function. That return User by user login request
        /// </summary>
        /// <param name="userLoginRequest"></param>
        /// <returns></returns>
        public Account UserLogin(LoginRequest userLoginRequest)
        {
            try
            {
                var passwordByMD5 = UtilityFunction.CreateMD5(userLoginRequest.Password);
                return Model.Where(row => row.Email == userLoginRequest.Email && row.Password == passwordByMD5).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// Check user register
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public bool CheckUserRegister(User user)
        //{
        //    try
        //    {
        //        var userlist = Model.Where(row => row.Username == user.Username || row.NumberPhone == user.NumberPhone).ToList();
        //        if(userlist.Count > 0)
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        
    }
}
