using System.Reflection;
using Core.Application.Services.Credits;
using Core.Application.Services.Genre;
using Core.Application.Services.Folders;
using Core.Application.Services.Movie;
using Core.Application.Services.People;
using Core.Application.Services.Popularity;
using Core.Application.Services.Search;
using Core.Application.Services.User;
using Core.Application.Services.Votes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ICreditsService, CreditsService>();
            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<IGenresService, GenreService>();
            services.AddScoped<IPopularityService, PopularityService>();
            services.AddScoped<IFolderService, FolderService>();
            services.AddScoped<IVoteService, VoteService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISearchService, SearchService>();
            
            return services;
        }
    }
}
