using LoyWms.Application.Authentication.Account;
using LoyWms.Application.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyWms.Application.Authentication.Interfaces;

public interface IAccountService
{
    //验证
    Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
    //注册
    Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
    //验证邮箱
    Task<Response<string>> ConfirmEmailAsync(string userId, string code);
    //忘记密码
    Task ForgotPassword(ForgotPasswordRequest model, string origin);
    //重设密码
    Task<Response<string>> ResetPassword(ResetPasswordRequest model);
}
