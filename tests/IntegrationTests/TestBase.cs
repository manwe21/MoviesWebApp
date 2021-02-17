using System;
using System.Collections.Generic;
using AutoMapper;
using Core.Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CreditsProfile = Core.Application.Data.AutoMapperConfiguration.Profiles.CreditsProfile;
using FolderProfile = Core.Application.Data.AutoMapperConfiguration.Profiles.FolderProfile;
using MovieProfile = Core.Application.Data.AutoMapperConfiguration.Profiles.MovieProfile;
using GenreProfile = Core.Application.Data.AutoMapperConfiguration.Profiles.GenreProfile;
using PeopleProfile = Core.Application.Data.AutoMapperConfiguration.Profiles.PeopleProfile;

namespace IntegrationTests
{
    public class TestBase
    {
        protected readonly IMapper Mapper;

        protected const string UserId1 = "895973FB-F8E1-4FD6-89C4-DC13CED4780E";
        protected const string UserId2 = "1AE05EF7-CE51-498A-B2B4-8E6A4DF8C0ED";

        protected TestBase()
        {
            Mapper = CreateMapper();
        }
        
        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //Core
                cfg.AddProfile(new MovieProfile());
                cfg.AddProfile(new CreditsProfile());
                cfg.AddProfile(new FolderProfile());
                cfg.AddProfile(new GenreProfile());
                cfg.AddProfile(new PeopleProfile());
                
                //Web
                cfg.AddProfile(new Web.Models.AutoMapperProfiles.MovieProfile());
                cfg.AddProfile(new Web.Models.AutoMapperProfiles.CreditsProfile());
                cfg.AddProfile(new Web.Models.AutoMapperProfiles.FolderProfile());
                cfg.AddProfile(new Web.Models.AutoMapperProfiles.PeopleProfile());
                cfg.AddProfile(new Web.Models.AutoMapperProfiles.SearchProfile());
            });

            return new Mapper(config);
        }
        
        protected MovieContext CreateAndSeedDb()
        {
            DbContextOptions<MovieContext> options = new DbContextOptionsBuilder<MovieContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            MovieContext db = new MovieContext(options);
            SeedDb(db);
            return db;
        }

        private void SeedDb(MovieContext db)
        {
            List<Movie> movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Movie 1", Rating = 8.3, VotesCount = 300, CreditId = 1, ReleaseDate = DateTime.Parse("29-01-2005") },
                new Movie { Id = 2, Title = "Movie 2", Rating = 9.1, VotesCount = 300, CreditId = 2, ReleaseDate = DateTime.Parse("01-02-2010") },
                new Movie { Id = 3, Title = "Movie 3", Rating = 5.5, VotesCount = 800, CreditId = 3, ReleaseDate = DateTime.Parse("21-10-1990") },
                new Movie { Id = 4, Title = "Movie 4", Rating = 8.0, VotesCount = 100, CreditId = 4, ReleaseDate = DateTime.Parse("12-08-1984") },
                new Movie { Id = 5, Title = "Movie 5", CreditId = 5, ReleaseDate = DateTime.Parse("21-10-2022") },
                new Movie { Id = 6, Title = "Movie 6", CreditId = 6, ReleaseDate = DateTime.Parse("12-08-2021") }
            };

            List<Genre> genres = new List<Genre>
            {
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Comedy" },
                new Genre { Id = 3, Name = "Drama"  }
            };

            List<MovieGenre> mgs = new List<MovieGenre>
            {
                new MovieGenre { MovieId = 1, GenreId = 1 },
                new MovieGenre { MovieId = 1, GenreId = 2 },
                new MovieGenre { MovieId = 2, GenreId = 1 },
                new MovieGenre { MovieId = 2, GenreId = 2 },
                new MovieGenre { MovieId = 2, GenreId = 3 },
                new MovieGenre { MovieId = 3, GenreId = 1 },
                new MovieGenre { MovieId = 3, GenreId = 3 },
                new MovieGenre { MovieId = 4, GenreId = 2 },
                new MovieGenre { MovieId = 4, GenreId = 3 }
            };

            List<Credit> credits = new List<Credit>
            {
                new Credit { Id = 1 },
                new Credit { Id = 2 },
                new Credit { Id = 3 },
                new Credit { Id = 4 },
                new Credit { Id = 5 },
                new Credit { Id = 6 }
            };

            List<Role> roles = new List<Role>
            {
                new Role { Id = 1, ActorId = 1, Character = "Character 1", CreditId = 1, Order = 1 },
                new Role { Id = 2, ActorId = 2, Character = "Character 2", CreditId = 1, Order = 2 },
                new Role { Id = 3, ActorId = 3, Character = "Character 3", CreditId = 2, Order = 1 },
                new Role { Id = 4, ActorId = 4, Character = "Character 4", CreditId = 2, Order = 3 },
                new Role { Id = 5, ActorId = 5, Character = "Character 5", CreditId = 2, Order = 2 },
                new Role { Id = 6, ActorId = 6, Character = "Character 6", CreditId = 2, Order = 4 },
                new Role { Id = 7, ActorId = 7, Character = "Character 7", CreditId = 3, Order = 2 },
                new Role { Id = 8, ActorId = 1, Character = "Character 8", CreditId = 3, Order = 1 },
                new Role { Id = 9, ActorId = 9, Character = "Character 9", CreditId = 3, Order = 3 }
            };

            List<Employee> crew = new List<Employee>
            {
                new Employee { Id = 1, PersonId = 2, Job = "Director", CreditId = 1, Department = "Directing" },
                new Employee { Id = 2, PersonId = 4, Job = "Writer", CreditId = 1, Department = "Writing" },
                new Employee { Id = 3, PersonId = 6, Job = "Director", CreditId = 2, Department = "Directing" },
                new Employee { Id = 4, PersonId = 8, Job = "Producer", CreditId = 2, Department = "Producing" },
                new Employee { Id = 5, PersonId = 1, Job = "Operator", CreditId = 2, Department = "Camera" },
                new Employee { Id = 6, PersonId = 1, Job = "Director", CreditId = 3, Department = "Directing" },
                new Employee { Id = 7, PersonId = 5, Job = "Producer", CreditId = 3, Department = "Producing" },
                new Employee { Id = 8, PersonId = 7, Job = "SoundDirector", CreditId = 3, Department = "Sound" }
            };

            List<Person> people = new List<Person>
            {
                new Person { Id = 1, Name = "Name Surname 1" },
                new Person { Id = 2, Name = "Name Surname 2" },
                new Person { Id = 3, Name = "Name Surname 3" },
                new Person { Id = 4, Name = "Name Surname 4" },
                new Person { Id = 5, Name = "Name Surname 5" },
                new Person { Id = 6, Name = "Name Surname 6" },
                new Person { Id = 7, Name = "Name Surname 7" },
                new Person { Id = 8, Name = "Name Surname 8" },
                new Person { Id = 9, Name = "Name Surname 9" }
            };

            List<Folder> folders = new List<Folder>
            {
                new Folder { Id = 1, Name = "Folder1", IsDefault = false, OwnerId = UserId1 },
                new Folder { Id = 2, Name = "Folder2", IsDefault = false, OwnerId = UserId2 },
                new Folder { Id = 3, Name = "Folder3", IsDefault = false, OwnerId = UserId1 }
            };

            List<Vote> votes = new List<Vote>
            {
                new Vote { UserId = UserId1, MovieId = 1, Value = 5 },
                new Vote { UserId = UserId1, MovieId = 2, Value = 10 },
                new Vote { UserId = UserId1, MovieId = 3, Value = 7 },
                new Vote { UserId = UserId2, MovieId = 1, Value = 1 },
                new Vote { UserId = UserId2, MovieId = 2, Value = 4 },
                new Vote { UserId = UserId2, MovieId = 3, Value = 9 },
                new Vote { UserId = UserId2, MovieId = 4, Value = 10 }
            };

            List<MovieFolder> mfs = new List<MovieFolder>
            {
                new MovieFolder { FolderId = 1, MovieId = 1 },
                new MovieFolder { FolderId = 3, MovieId = 1 },
                new MovieFolder { FolderId = 3, MovieId = 2 },
                new MovieFolder { FolderId = 3, MovieId = 3 }
            };

            db.Movies.AddRange(movies);
            db.Genres.AddRange(genres);
            db.MovieGenres.AddRange(mgs);
            db.Credits.AddRange(credits);
            db.Cast.AddRange(roles);
            db.Crew.AddRange(crew);
            db.People.AddRange(people);
            db.Folders.AddRange(folders);
            db.MovieFolders.AddRange(mfs);
            db.Votes.AddRange(votes);
            
            db.SaveChanges();
        }
    }
}
