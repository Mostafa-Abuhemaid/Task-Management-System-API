
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Web.Application.DTOs.EmailDTO;
using Web.Application.Interfaces;
using Web.Application.Interfaces.ExternalAuthService;
using Web.Application.Mapping;
using Web.Domain.Entites;
using Web.Domain.Hubs;
using Web.Infrastructure.Data;
using Web.Infrastructure.Service;
using Web.Infrastructure.Service.ExternalAuthService;

namespace Web.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            // Add services to the container.

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<AppUser, IdentityRole>()
             .AddRoles<IdentityRole>()
             .AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders();
            builder.Services.AddSignalR();
            builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            builder.Services.Configure<EmailDto>(configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IGoogleService, GoogleService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<IUserService,UserService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddSingleton<INotificationService, NotificationService>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddMemoryCache();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(o =>
           {
               o.RequireHttpsMetadata = false;
               o.SaveToken = false;
               o.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),

                   ValidateIssuer = true,
                   ValidIssuer = configuration["JWT:Issuer"],

                   ValidateAudience = true,
                   ValidAudience = configuration["JWT:Audience"],

                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero
               };
           }).AddGoogle("Google", options =>
           {
               options.ClientId = "670016602508-18rt5v58f515kks4b3qctp4kqpbpc32l.apps.googleusercontent.com";
               options.ClientSecret ="GOCSPX - LuYp5dLq_1YGOWdVq7h1IrcMpfH9";
               options.CallbackPath = "/signin-google";
           });

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
           

            app.MapControllers();
            app.UseCors("CorsPolicy");

            app.MapHub<NotificationHub>("/notificationHub");

            app.Run();
        }
    }
}
