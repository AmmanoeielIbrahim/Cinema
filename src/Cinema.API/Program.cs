using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories;
using Scalar.AspNetCore;
using Services.Implementation;
using Services.ProfileMapping;
using ServicesAbstraction.Interfaces;

namespace Cinema.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddDbContext<CinemaDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        builder.Services.AddScoped<IDataSeeder, DataSeeder>();
        builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireDigit = true;
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<CinemaDbContext>()
            .AddDefaultTokenProviders();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IAttachmentService, AttachmentService>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddAutoMapper(temp => { }, typeof(AuthenticationProfile).Assembly);

        #endregion Add services to the container.

        var app = builder.Build();
        using var scoped = app.Services.CreateScope();
        var objectOfDataSeed = scoped.ServiceProvider.GetRequiredService<IDataSeeder>();
        objectOfDataSeed.DataSeed();

        #region Middlewares

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.MapControllers();

        #endregion Middlewares

        app.Run();
    }
}