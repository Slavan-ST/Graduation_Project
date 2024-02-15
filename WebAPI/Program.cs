
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication.Handlers;
using WebAPI.Authentication.Requirements;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddAuthorization(options =>
            {
                //сюда можно лепить политики доступа
                options.AddPolicy("user", policy => policy.Requirements.Add(new AccessRequirement("User")));
                options.AddPolicy("admin", policy =>
                {
                    policy.Requirements.Add(new AccessRequirement("Admin"));
                });
                options.AddPolicy("moderator", policy => policy.Requirements.Add(new AccessRequirement("Moderator")));
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = "/login");

            builder.Services.AddCors(); // добавляем сервисы CORS
            builder.Services.AddSingleton<IAuthorizationHandler, AccessHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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
