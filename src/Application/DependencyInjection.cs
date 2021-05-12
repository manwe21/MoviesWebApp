using System.Reflection;
using Application.Services.Credits;
using Application.Services.Folders;
using Application.Services.Genre;
using Application.Services.Movie;
using Application.Services.People;
using Application.Services.Popularity;
using Application.Services.Search;
using Application.Services.User;
using Application.Services.Votes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
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
