using System;
using System.Collections.Generic;
using System.Text;
using Application;
using Application.Data;
using Application.Events;
using Infrastructure.Api;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)    
        {
            services.AddDbContext<MovieContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MoviesConnection")));
            services.AddDbContext<AppIdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddScoped<IMovieContext>(provider => provider.GetService<MovieContext>());
            services.AddScoped<ISearchDataService, SearchDataService>();
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddUrlBuilder(options =>
            {
                options.ApiKey = configuration["TMDBApiKey"];
            });
            return services;
        }
    }
}
