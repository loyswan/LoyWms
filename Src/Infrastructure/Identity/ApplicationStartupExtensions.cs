using LoyWms.Infrastructure.Identity.Models;
using LoyWms.Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LoyWms.Infrastructure.Identity;

public static class ApplicationStartupExtensions
{

    public static async Task IdentityDatabaseSeed(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            if (!userManager.Users.Any())// 添加种子数据
            {
                await DefaultRoles.SeedAsync(userManager, roleManager);
                await DefaultSuperAdmin.SeedAsync(userManager, roleManager);
                await DefaultBasicUser.SeedAsync(userManager, roleManager);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred migrating the DB: {ex.Message}");
        }
    }
}