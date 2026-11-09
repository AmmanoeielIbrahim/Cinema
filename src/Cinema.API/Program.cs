using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence.Data;
using Persistence.Global;
using Persistence.Repositories;
using Scalar.AspNetCore;
using Services.Implementation;
using Services.ProfileMapping;
using ServicesAbstraction.Interfaces;
using Share;
using System.Text;

namespace Cinema.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var Services = builder.Services;
        var Configuration = builder.Configuration;

        #region Add services to the container.

        Services.AddControllers();
        Services.AddOpenApi();
        Services.AddDbContext<CinemaDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        });
        Services.AddScoped<IDataSeeder, DataSeeder>();
        Services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireDigit = true;
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<CinemaDbContext>()
            .AddDefaultTokenProviders();
        Services.AddScoped<IUnitOfWork, UnitOfWork>();
        Services.AddScoped<IAttachmentService, AttachmentService>();
        Services.AddScoped<IAuthenticationService, AuthenticationService>();
        Services.AddAutoMapper(temp => { }, typeof(AuthenticationProfile).Assembly);
        Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
        Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration.GetRequiredSection("JWT")["Issuer"],
                ValidAudience = Configuration.GetRequiredSection("JWT")["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetRequiredSection("JWT")["SecretKey"])),
                ClockSkew = TimeSpan.Zero
            };
        });
        Services.AddScoped<IRefreshToken, Persistence.Repositories.RefreshToken>();

        #endregion Add services to the container.

        var app = builder.Build();
        using var scoped = app.Services.CreateScope();
        var objectOfDataSeed = scoped.ServiceProvider.GetRequiredService<IDataSeeder>();
        await objectOfDataSeed.DataSeed();

        #region Middlewares

        app.UseMiddleware<GlobalExceptionMiddleware>();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        #endregion Middlewares

        app.Run();
    }
}