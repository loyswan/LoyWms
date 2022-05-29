using LoyWms.Application.Authentication.Account;

namespace LoyWms.Application.Authentication.Interfaces;

public interface IEmailService
{
    Task SendAsync(EmailRequest request);
}