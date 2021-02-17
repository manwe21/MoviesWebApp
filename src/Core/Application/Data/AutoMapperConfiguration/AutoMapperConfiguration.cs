using AutoMapper;
using Core.Application.Data.AutoMapperConfiguration.Profiles;

namespace Core.Application.Data.AutoMapperConfiguration
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration Config { get; }
        
        static AutoMapperConfiguration()
        {
            Config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MovieProfile>();
                cfg.AddProfile<CreditsProfile>();
                cfg.AddProfile<FolderProfile>();
                cfg.AddProfile<GenreProfile>();
                cfg.AddProfile<PeopleProfile>();
            });   
        }

    }
}    
