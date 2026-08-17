
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onion.APIs.Errors;
using Onion.APIs.Extensions;
using Onion.APIs.Helper;
using Onion.APIs.MiddleWares;
using Onion.Core.Repositories;
using Onion.Repository;
using Onion.Repository.Data;
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
            builder.Services.AddDbContext<OnionContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
            {
                var Connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(Connection);
            });
            builder.Services.AddAplicationServices();
           var app = builder.Build();
            
            var Scope = app.Services.CreateScope();
            var Services = Scope.ServiceProvider;
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var DbContext = Services.GetRequiredService<OnionContext>();
                //OnionContext dbContext = new OnionContext();
                await DbContext.Database.MigrateAsync();
                await OnionContextSeed.SeedAsync(DbContext);
            }catch(Exception ex)
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

           


            app.MapControllers();

            app.Run();
        }
    }
}
