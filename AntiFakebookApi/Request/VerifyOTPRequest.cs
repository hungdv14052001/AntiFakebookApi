﻿namespace AntiFakebookApi.Request
{
    public class VerifyOTPRequest
    {
        public string Email { get; set; }
        public string Otp { get; set; }
    }
}
