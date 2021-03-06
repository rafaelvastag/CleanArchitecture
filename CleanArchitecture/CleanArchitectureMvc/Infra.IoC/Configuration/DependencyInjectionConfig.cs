using Application.Mappings;
using Application.Services;
using Application.Services.Impl;
using Domain.Authentication;
using Domain.Interfaces;
using Infra.Data.Context;
using Infra.Data.Identity;
using Infra.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infra.IoC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var myHandlers = AppDomain.CurrentDomain.Load("Application");

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
             b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options => options.AccessDeniedPath = "/Account/Login");

            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<ISeedUserRoleInitialService, SeedUserRoleInitialService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddMediatR(myHandlers);

            services.AddAutoMapper(typeof(EntityToDTOMappingProfile));
            services.AddAutoMapper(typeof(DTOToCommandMappingProfile));

            return services;
        }
    }
}
