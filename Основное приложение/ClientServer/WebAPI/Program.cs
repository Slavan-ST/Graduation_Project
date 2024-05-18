
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Security.Handlers;
using WebAPI.Security.Requirements;

namespace WebAPI
{
    /// <summary>
    /// Основной класс программы
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Метод входа в программу
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(); // добавляем сервисы CORS
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {

                var basePath = AppContext.BaseDirectory;

                var xmlPath = Path.Combine(basePath, "WebAPI.xml");
                options.IncludeXmlComments(xmlPath);

                options.SwaggerDoc("v2.0", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v2.0",
                    Title = "Документация API SystemO",
                });
            });

            builder.Services.AddAuthorizationBuilder()
                .AddPolicy("user", policy => policy.Requirements.Add(new AccessRequirement("User")))
                .AddPolicy("admin", policy =>
                {
                    policy.Requirements.Add(new AccessRequirement("Admin"));
                })
                .AddPolicy("moderator", policy => policy.Requirements.Add(new AccessRequirement("Moderator"))); //авторизация, добавление политик доступа
            
            
            builder.Services.AddSingleton<IAuthorizationHandler, AccessHandler>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/login");


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger()
                    .UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v2.0/swagger.json", "v2.0");
                    });
            }

            // настраиваем CORS
            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseAuthentication();   // добавление middleware аутентификации 
            app.UseAuthorization();   // добавление middleware авторизации 


            app.MapControllers();


            app.Run();
        }
    }
}
