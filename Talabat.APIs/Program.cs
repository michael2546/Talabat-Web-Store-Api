using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services - Add services to the container

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //allow debendency Injectin for my StoreContext Class
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            //DefaultConnection
            }) ;

            builder.Services.addExtentionServices();  //extention method i made it

            #endregion

            var app = builder.Build();

            #region Update Database Migrations

            using var scope = app.Services.CreateScope();
            var Services = scope.ServiceProvider;

            var loggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                //StoreContext dbContext = new StoreContext();
                //await dbContext.Database.MigrateAsync();
                var dbContext = Services.GetRequiredService<StoreContext>();
                await dbContext.Database.MigrateAsync();

                // Data Seeding
                await StoreContextSeed.SeedAsync(dbContext);
  
            }
            catch (Exception ex)
            {
                var Logger = loggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "Error during Updating Database Migration");
            }


            #endregion


            // Configure the HTTP request pipeline.
            #region Configure - Configure the http request pipeline

            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleWare>();
                app.UseSwaggerMiddlewares(); //extention method i made it
            }
            app.UseStaticFiles();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            //the kestrel will send the value of {0} and go to the route errors controller

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers(); 

            #endregion

            app.Run();
        }
    }
}