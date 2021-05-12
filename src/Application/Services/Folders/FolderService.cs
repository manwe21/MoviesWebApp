using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Data.AutoMapperConfiguration;
using Application.Data.QueryExtensions.Folders;
using Application.Data.QueryExtensions.Movies;
using Application.Data.QueryExtensions.Pagination;
using Application.Dto;
using Application.Exceptions.HttpExceptions;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Folders
{
    public class FolderService : IFolderService
    {
        private readonly IMovieContext _db;
        
        public FolderService(IMovieContext db)
        {
            _db = db;
        }

        public async Task<List<FolderDto>> ListFoldersForUserAsync(string userId)
        {
            return await _db.Folders
                .Where(f => f.OwnerId == userId)
                .ProjectTo<FolderDto>(AutoMapperConfiguration.Config)
                .ToListAsync();
        }
                
        public async Task<PagedResult<MovieWithVoteDto>> ListMoviesFromFolderAsync(int folderId, string userId, int page, int pageSize)
        {
            var movies = await _db.Movies
                .GetMoviesFromFolder(folderId)
                .ProjectTo<MovieWithVoteDto>(AutoMapperConfiguration.Config, new {userId})
                .PaginateAsync(page, pageSize);
            return movies;
        }

        public async Task<PagedResult<MovieWithGuestVoteDto>> ListMoviesFromFolderForCoupleAsync(int folderId, string ownerId, string guestId, int page, int pageSize)
        {
            var movies = await _db.Movies
                .GetMoviesFromFolder(folderId)
                .ProjectTo<MovieWithGuestVoteDto>(AutoMapperConfiguration.Config, new {userId = guestId, ownerId})
                .PaginateAsync(page, pageSize);
            return movies;
        }

        public async Task CreateDefaultFoldersForUserAsync(string userId)
        {
            var favoriteFolder = new Folder
            {
                Name = FolderName.FavoriteMovies,
                IsDefault = true,
                OwnerId = userId
            };  
            var watchLaterFolder = new Folder
            {
                Name = FolderName.WatchLater,
                IsDefault = true,
                OwnerId = userId
            };

            await _db.Folders.AddRangeAsync(favoriteFolder, watchLaterFolder);
            await _db.SaveChangesAsync();
        }
            
        public async Task<int> CreateFolderForUserAsync(string userId, string folderName)
        {
            if (_db.Folders.IsFolderWithNameAlreadyExists(userId, folderName))
                throw new BadRequestException();

            var folder = new Folder
            {
                OwnerId = userId,
                IsDefault = false,
                Name = folderName
            };

            await _db.Folders.AddAsync(folder);
            await _db.SaveChangesAsync();
            return folder.Id;
        }

        public async Task DeleteFolderAsync(string userId, int folderId)
        {
            var folder = await _db.Folders.FindAsync(folderId);

            if (folder == null)
                throw new ResourceNotFoundException();
            if (folder.OwnerId != userId)
                throw new AccessDeniedException();
            if (folder.IsDefault)
                throw new BadRequestException();

            _db.Folders.Remove(folder);
            await _db.SaveChangesAsync();
        }

        public async Task RenameFolderAsync(string userId, int folderId, string newName)
        {
            var folder = await _db.Folders.FindAsync(folderId);

            if (folder == null)
                throw new ResourceNotFoundException();
            if (folder.OwnerId != userId)
                throw new AccessDeniedException();
            if (_db.Folders.IsFolderWithNameAlreadyExists(folder.OwnerId, newName))
                throw new BadRequestException();
            if (folder.IsDefault)
                throw new BadRequestException();

            folder.Name = newName;
            await _db.SaveChangesAsync();
        }

        public async Task<List<FolderDto>> GetFoldersWithMovieAsync(string userId, int movieId)
        {
            return await _db.Folders
                .GetFoldersWithMovie(userId, movieId)
                .ProjectTo<FolderDto>(AutoMapperConfiguration.Config)
                .ToListAsync();
        }

        public async Task AddMovieToFolderAsync(string userId, int folderId, int movieId)
        {
            var mf = await _db.MovieFolders.FindAsync(movieId, folderId);
            if (mf != null)
                throw new BadRequestException();
            
            var folder = await _db.Folders.FindAsync(folderId);
            if (folder == null)
                throw new ResourceNotFoundException();
            
            if(folder.OwnerId != userId)
                throw new AccessDeniedException();
            
            var movie = await _db.Movies.FindAsync(movieId);
            if (movie == null)
                throw new ResourceNotFoundException();
            
            await _db.MovieFolders.AddAsync(new MovieFolder { FolderId = folderId, MovieId = movieId });
            await _db.SaveChangesAsync();
        }

        public async Task DeleteMovieFromFolderAsync(string userId, int folderId, int movieId)
        {
            var mf = await _db.MovieFolders.FindAsync(movieId, folderId);
            if (mf == null)
                throw new BadRequestException();
                
            var folder = await _db.Folders.FindAsync(folderId);
            if(folder.OwnerId != userId)
                throw new AccessDeniedException();
            
            _db.MovieFolders.Remove(mf);
            await _db.SaveChangesAsync();
        }
    }
}
