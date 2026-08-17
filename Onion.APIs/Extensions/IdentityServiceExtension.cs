using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Onion.Core.Entities.Identity;
using Onion.Repository.Identity;
using Onion.Services;

namespace Onion.APIs.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection Services)
        {
            Services.AddScoped<ITokenService, TokenService>();
            Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            Services.AddAuthentication();
            Services.AddAplicationServices();
            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            return Services;
        } 
    }
}
