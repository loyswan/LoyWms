using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyWms.Application.Authentication.Account;

//验证请求
public class AuthenticationRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
