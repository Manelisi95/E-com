using API.Errors;
using System.Linq;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>),(typeof(GenericRepository<>)));

             services.Configure<ApiBehaviorOptions>(options => 
            {
                options.InvalidModelStateResponseFactory = ActionContext => 
                {
                    var errors = ActionContext.ModelState.Where(x => x.Value.Errors.Count > 0)
                    .SelectMany(e => e.Value.Errors)
                    .Select(y => y.ErrorMessage).ToArray();

                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

           return services;
        }
    }
}