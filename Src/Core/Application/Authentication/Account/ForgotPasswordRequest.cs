using System.ComponentModel.DataAnnotations;

namespace LoyWms.Application.Authentication.Account;

//忘记密码请求
public class ForgotPasswordRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
