using LoyWms.Application.Authentication.Account;
using LoyWms.Application.Authentication.Enums;
using LoyWms.Application.Authentication.Interfaces;
using LoyWms.Application.Common.Exceptions;
using LoyWms.Application.Common.Wrappers;
using LoyWms.Domain.Settings;
using LoyWms.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoyWms.Infrastructure.Identity.Services;

public class AccountService : IAccountService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JWTSettings _jwtSettings;
    //private readonly IDateTimeService _dateTimeService;
    //private readonly IEmailService _emailService;

    public AccountService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        IOptions<JWTSettings> jwtSettings)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
    }

    #region 验证账户

    public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
    {
        //获取用户，验证账户
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
        {
            throw new ApiException($"No Accounts Registered with {request.UserName}.");
        }
        //验证账号的密码
        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            throw new ApiException($"Invalid Credentials for '{user.UserName}'.");
        }
        //判断邮箱是否验证
        //if (!user.EmailConfirmed)
        //{
        //    throw new ApiException($"Account Not Confirmed for '{user.Email}'.");
        //}

        //生成用户的Token
        var jwtSecurityToken = await GenerateJWToken(user);
        //生成用户的RefreshToken
        var refreshToken = GenerateRefreshToken(ipAddress);

        //获取用户的角色列表
        var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

        //创建登录响应
        var response = new AuthenticationResponse()
        {
            Id = user.Id,
            JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName,
            RefreshToken = refreshToken.Token,
            Roles = rolesList.ToList(),
            IsVerified = user.EmailConfirmed
        };

        return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
    }

    private RefreshToken GenerateRefreshToken(string ipAddress)
    {
        var randomBytes = RandomNumberGenerator.GetBytes(32);
        var token = Convert.ToBase64String(randomBytes);

        return new RefreshToken()
        {
            Token = token,
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };
    }

    private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role));
        }

        //string ipAddress = IpHelper.GetIpAddress();
        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)//,
                //new Claim("ip", ipAddress)
            }
        .Union(userClaims).Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            //
            claims: claims,
            //有效时间
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            //加密算法信息
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }

    #endregion


    #region 注册账户
    public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
    {
        //判断用户名是否注册
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user != null)
        {
            throw new ApiException($"Username '{request.UserName}' is already taken.");
        }
        //判断邮箱是否注册
        user = await _userManager.FindByEmailAsync(request.Email);
        if (user != null)
        {
            throw new ApiException($"Email {request.Email } is already registered.");
        }

        //新建账户
        var newUser = new ApplicationUser()
        {
            Email = request.Email,
            UserName = request.UserName,
        };
        //注册
        var result = await _userManager.CreateAsync(newUser, request.Password);
        if (result.Succeeded)
        {
            //账号默认角色为基本角色
            await _userManager.AddToRoleAsync(newUser, Roles.Basic.ToString());
            //验证用户邮箱（发送邮件）暂时不验证邮箱
            //返回创建成功响应
            return new Response<string>(newUser.Id, $"用户“{newUser.UserName}”创建成功");
        }
        else
        {
            throw new ApiException($"{result.Errors}");
        }

    }

    #endregion

    #region 验证账户邮箱

    public Task<Response<string>> ConfirmEmailAsync(string userId, string code)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region 忘记密码与修改密码
    public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
    {
        //var account = await _userManager.FindByEmailAsync(model.Email);
        //if (account == null) return;

        //var code = await _userManager.GeneratePasswordResetTokenAsync(account);
        //var route = "api/account/reset-password/";
        //var _enpointUri = new Uri(string.Concat($"{origin}/", route));
        //var emailRequest = new EmailRequest()
        //{
        //    Body = $"You reset token is - {code}",
        //    To = model.Email,
        //    Subject = "Reset Password",
        //};
        //await _emailService.SendAsync(emailRequest);
        throw new NotImplementedException();
    }

    public async Task<Response<string>> ResetPassword(ResetPasswordRequest model)
    {
        var account = await _userManager.FindByEmailAsync(model.Email);
        if (account == null) throw new ApiException($"No Accounts Registered with {model.Email}.");
        var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
        if (result.Succeeded)
        {
            return new Response<string>(model.Email, message: $"Password Resetted.");
        }
        else
        {
            throw new ApiException($"Error occured while reseting the password.");
        }
    }
    #endregion
}
