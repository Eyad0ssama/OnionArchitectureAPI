
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onion.APIs.Errors;
using Onion.APIs.Extensions;
using Onion.APIs.Helper;
using Onion.APIs.MiddleWares;
using Onion.Core.Entities.Identity;
using Onion.Core.Repositories;
using Onion.Repository;
using Onion.Repository.Data;
using Onion.Repository.Identity;
using StackExchange.Redis;

namespace Onion.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<OnionContext>(
                options => 
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                }
            );
            builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
            {
                var Connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(Connection);
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(
                options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
                }
                );
            builder.Services.AddIdentityService(builder.Configuration);
            //builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            //builder.Services.AddAuthentication();
            // builder.Services.AddAplicationServices();
           var app = builder.Build();
            
           using var Scope = app.Services.CreateScope();
            var Services = Scope.ServiceProvider;
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var DbContext = Services.GetRequiredService<OnionContext>();
                await DbContext.Database.MigrateAsync();

                var IdentityDbContext = Services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync();

                var userManger = Services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsynk(userManger);
                await OnionContextSeed.SeedAsync(DbContext);
            }
            catch(Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An Error Occeurd During Appling Migration");
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptioMiddleWares>();
                app.UseSwaggerMiddlwares();
            }
            app.UseStatusCodePagesWithRedirects("/errors/{0}");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
           


            app.MapControllers();

            app.Run();
        }
    }
}
