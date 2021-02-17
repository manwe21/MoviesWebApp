using System.Security.Principal;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using CoreCreditsProfile = Core.Application.Data.AutoMapperConfiguration.Profiles.CreditsProfile;
using CoreFolderProfile = Core.Application.Data.AutoMapperConfiguration.Profiles.FolderProfile;
using CoreMovieProfile = Core.Application.Data.AutoMapperConfiguration.Profiles.MovieProfile;
using CoreGenreProfile = Core.Application.Data.AutoMapperConfiguration.Profiles.GenreProfile;
using CorePeopleProfile = Core.Application.Data.AutoMapperConfiguration.Profiles.PeopleProfile;

using WebCreditsProfile = Web.Models.AutoMapperProfiles.CreditsProfile;
using WebMovieProfile = Web.Models.AutoMapperProfiles.MovieProfile;
using WebFolderProfile = Web.Models.AutoMapperProfiles.FolderProfile;
using WebPeopleProfile = Web.Models.AutoMapperProfiles.PeopleProfile;
using WebSearchProfile =  Web.Models.AutoMapperProfiles.SearchProfile;

namespace UnitTests
{
    public class TestBase
    {
        protected readonly IMapper Mapper;

        protected TestBase()
        {
            Mapper = CreateMapper();
        }
        
        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //Core
                cfg.AddProfile(new CoreMovieProfile());
                cfg.AddProfile(new CoreCreditsProfile());
                cfg.AddProfile(new CoreFolderProfile());
                cfg.AddProfile(new CoreGenreProfile());
                cfg.AddProfile(new CorePeopleProfile());
                
                //Web
                cfg.AddProfile(new WebMovieProfile());
                cfg.AddProfile(new WebCreditsProfile());
                cfg.AddProfile(new WebFolderProfile());
                cfg.AddProfile(new WebPeopleProfile());
                cfg.AddProfile(new WebSearchProfile());
            });

            return new Mapper(config);
        }

        protected static PageContext CreatePageContext(bool isAuthenticated)
        {
            var fakeContext = new Mock<HttpContext>();
            var fakeIdentity = new Mock<IIdentity>();
            fakeIdentity.Setup(i => i.IsAuthenticated).Returns(isAuthenticated);
            var principal = new GenericPrincipal(fakeIdentity.Object, null);
            fakeContext.Setup(x => x.User).Returns(principal);
            
            return new PageContext
            {
                HttpContext = fakeContext.Object
            };
        }
        
    }
}