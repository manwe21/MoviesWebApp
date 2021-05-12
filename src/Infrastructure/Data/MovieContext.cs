using System.Linq;
using System.Threading.Tasks;
using Core.Application;
using Core.Application.Data;
using Core.Application.Events;
using Core.Domain.Common;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data
{
    public class MovieContext : DbContext, IMovieContext
    {
        private readonly IDomainEventService _domainEventService;
        
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Employee> Crew { get; set; }
        public DbSet<Role> Cast { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<MovieFolder> MovieFolders { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<SearchItem> SearchItems { get; set; }
        
        public MovieContext(DbContextOptions<MovieContext> options, IDomainEventService domainEventService) : base(options)
        {
            _domainEventService = domainEventService;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }

        public void CommitTransaction() 
        {
            Database.CommitTransaction();
        }

        public async Task<int> SaveChangesAsync()
        {
            var res = await base.SaveChangesAsync();
            await DispatchDomainEvents();
            return res;
        }

        private async Task DispatchDomainEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker
                    .Entries<BaseEntity>()
                    .Select(x => x.Entity.Events)
                    .SelectMany(x => x)
                    .FirstOrDefault(domainEvent => !domainEvent.IsPublished);
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasKey(m => m.Id);
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Credit)
                .WithOne(c => c.Movie)
                .HasForeignKey<Movie>(m => m.CreditId);

            modelBuilder.Entity<Role>().ToTable("Cast");
            modelBuilder.Entity<Role>().Property(r => r.ActorId).HasColumnName("PersonId");
            modelBuilder.Entity<Role>().HasKey(r => r.Id);
            modelBuilder.Entity<Role>()
                .HasOne(r => r.Credit)
                .WithMany(c => c.Cast)
                .HasForeignKey(r => r.CreditId);
            modelBuilder.Entity<Role>()
                .HasOne(r => r.Actor)
                .WithMany(p => p.Roles)
                .HasForeignKey(r => r.ActorId);

            modelBuilder.Entity<Employee>().ToTable("Crew");
            modelBuilder.Entity<Employee>().HasKey(r => r.Id);
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Credit)
                .WithMany(c => c.Crew)
                .HasForeignKey(e => e.CreditId);
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Person)
                .WithMany(p => p.Jobs)
                .HasForeignKey(e => e.PersonId);

            modelBuilder.Entity<MovieGenre>().ToTable("MoviesGenres");
            modelBuilder.Entity<MovieGenre>().HasKey(mg => new { mg.MovieId, mg.GenreId });
            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);
            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId);

            modelBuilder.Entity<Person>().Property(p => p.ImagePath).HasColumnName("PhotoPath");

            modelBuilder.Entity<MovieFolder>().HasKey(mf => new {mf.MovieId, mf.FolderId});
            modelBuilder.Entity<MovieFolder>().ToTable("MoviesFolders");
            modelBuilder.Entity<MovieFolder>()
                .HasOne(mf => mf.Folder)
                .WithMany(f => f.MovieFolders)
                .HasForeignKey(mf => mf.FolderId);

            modelBuilder.Entity<MovieFolder>()
                .HasOne(mf => mf.Movie)
                .WithMany(m => m.MovieFolders)
                .HasForeignKey(mf => mf.MovieId);

                modelBuilder.Entity<Folder>().OwnsOne(f => f.Name, builder =>
                {
                    builder.Property(n => n.Value).HasColumnName("Name");
                    builder.Property(f => f.Value).HasColumnType("string");
                    builder.Property(u => u.Value).IsRequired();
                });

            modelBuilder.Entity<Vote>().HasKey(r => new { r.UserId, r.MovieId });
            modelBuilder.Entity<Vote>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Votes)
                .HasForeignKey(r => r.MovieId);

            modelBuilder.Entity<SearchItem>().HasNoKey();

            modelBuilder.Entity<DomainEvent>().HasNoKey();
        }
    }
}
