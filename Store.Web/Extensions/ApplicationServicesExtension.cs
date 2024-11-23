using System.Runtime.CompilerServices;
using Store.Repository.Interfaces;
using Store.Service.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.Products.Dtos;
using Store.Service.HandleResponse;
using Store.Service.Services.CachService;
using Store.Repository.Basket;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.Dtos;

namespace Store.Web.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddSingleton<ICachService , CachService>();
            services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddScoped<IBasketService,BasketService>();
            services.AddAutoMapper(typeof(BasketProfile));
            services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var error = actionContext.ModelState
                    .Where(model => model.Value?.Errors.Count > 0)
                    .SelectMany(model => model.Value?.Errors)
                    .Select(error => error.ErrorMessage)
                    .ToList();
                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = error
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}
