using System;
using AutoMapper;
using Core.Application;
using Core.Application.Data.Repositories;
using Infrastructure;
using Infrastructure.Data.Repositories;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Web.Middlewares;
using Web.Models.Person;
using Web.Services;

namespace Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityContext>();
            services.AddHttpClient<IPopularityRepository, ApiPopularityRepository>(c =>
            {
                c.BaseAddress = new Uri(Configuration["ApiTMDBAddress"]);
            });
            services.AddApplication();
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/register";
            });

            services.AddScoped<IFilmographyViewModelService, FilmographyViewModelService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandling();
                app.UseStatusCodePages();
            }
            
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseMiddleware<RequestInitiatorMiddleware>();

            app.UseEndpoints(routes =>
            {
                routes.MapControllers();
                routes.MapRazorPages();
            });
        }
    }
}
