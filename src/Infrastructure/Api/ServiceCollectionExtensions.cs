using System;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Api
{   
    public static class ServiceCollectionExtensions
    {
        public static void AddUrlBuilder(this IServiceCollection services, Action<UrlBuilderOptions> setupAction)
        {
            services.AddScoped<TmdbUrlBuilder>();
            services.Configure(setupAction);
        }
    }   
}
