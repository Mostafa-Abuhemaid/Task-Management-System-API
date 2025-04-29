
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Web.Application.DTOs.EmailDTO;
using Web.Application.Interfaces;
using Web.Application.Mapping;
using Web.Domain.Entites;
using Web.Infrastructure.Data;
using Web.Infrastructure.Service;

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
            builder.Services.Configure<EmailDto>(configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddMemoryCache();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
