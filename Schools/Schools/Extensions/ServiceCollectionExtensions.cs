﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Schools.Data;
using Schools.Data.Models;

namespace Schools.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<ApplicationDbContext>(options => options
                       .UseSqlServer(configuration.GetDefaultConnectionString()));

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
      services
          .AddIdentity<User, IdentityRole>(options =>
          {
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
          })
          .AddEntityFrameworkStores<ApplicationDbContext>();

      return services;
    }

    public static IServiceCollection AddCookieAuthentication(this IServiceCollection services)
    {
      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => {
          options.Cookie.Name = "ApplicationCookie";
          options.LoginPath = "/Account/Login";
          options.LogoutPath = "/Account/Logout";
        });

      return services;
    }
  }
}