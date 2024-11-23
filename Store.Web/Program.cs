
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Store.Data.Context;
using Store.Repository.Interfaces;
using Store.Service.HandleResponse;
using Store.Service.Services.Products;
using Store.Service.Services.Products.Dtos;
using Store.Web.Extensions;
using Store.Web.Helper;
using Store.Web.Middleware;

namespace Store.Web
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
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var Configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(Configuration);
            });

            //builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            //builder.Services.AddScoped<IProductService,ProductService>();
            //builder.Services.AddAutoMapper(typeof(ProductProfile));

            ////builder.Services.Configure<CustomException>(Options => 


            ////)
            builder.Services.ApplicationServices();
            builder.Services.AddIdentityServices();
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddelware>();
            app.UseAuthorization();
            app.UseStaticFiles();

            await ApplySeeding.applySeedingAsync(app);

            app.MapControllers();

            app.Run();
        }
    }
}
