using AutoMapper;
using AutoMapper.Configuration;
using AntiFakebookApi.Common;
using AntiFakebookApi.Controllers;
using AntiFakebookApi.Database;
using AntiFakebookApi.Dto;
using AntiFakebookApi.Request;
using AntiFakebookApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace AntiFakebookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : BaseApiController<AuthenticateController>
    {
        private readonly UserAuthenticateService _userAuthenticateService;
        public AuthenticateController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig)
        {
            _userAuthenticateService = new UserAuthenticateService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public MessageData Login(LoginRequest request)
        {
            try
            {
                var res = _userAuthenticateService.Login(request);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// User register account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Signup")]
        public MessageData Signup(SignupRequest request)
        {
            try
            {
                var res = _userAuthenticateService.Signup(request);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// change password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangePassword")]
        public MessageData ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                var res = _userAuthenticateService.ChangePassword(AccountId, request);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        ///// <summary>
        ///// forgot password
        ///// </summary>
        ///// <param name="username"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[AllowAnonymous]
        //[Route("ForgotPassword")]
        //public MessageData ForgotPassword(string username)
        //{
        //    try
        //    {
        //        var res = _userAuthenticateService.ForgotPassword(username);
        //        return new MessageData { Data = res, Status = 1 };
        //    }
        //    catch (Exception ex)
        //    {
        //        return NG(ex);
        //    }
        //}

        ///// <summary>
        ///// Verify OTP
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("VerifyOTP")]
        //public MessageData VerifyOTP(VerifyOTPRequest request)
        //{
        //    try
        //    {
        //        var res = _userAuthenticateService.VerifyOTP(request);
        //        return new MessageData { Data = res, Status = 1 };
        //    }
        //    catch (Exception ex)
        //    {
        //        return NG(ex);
        //    }
        //}

        ///// <summary>
        ///// reset password
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[AllowAnonymous]
        //[Route("ResetPassword")]
        //public MessageData ResetPassword(ResetPasswordRequest request)
        //{
        //    try
        //    {
        //        var res = _userAuthenticateService.ResetPassword(request);
        //        return new MessageData { Data = res, Status = 1 };
        //    }
        //    catch (Exception ex)
        //    {
        //        return NG(ex);
        //    }
        //}
    }
}
