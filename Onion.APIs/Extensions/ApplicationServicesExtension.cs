using Microsoft.AspNetCore.Mvc;
using Onion.APIs.Controllers;
using Onion.APIs.Errors;
using Onion.APIs.Helper;
using Onion.Core.Repositories;
using Onion.Repository;

namespace Onion.APIs.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));
            Services.AddAutoMapper(m => m.AddProfile(new MappingProfile()));
            Services.AddScoped<ProductPictureUrlResolver>();
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count > 0).
                    SelectMany(p => p.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });
            return Services;
        }
    }
}
