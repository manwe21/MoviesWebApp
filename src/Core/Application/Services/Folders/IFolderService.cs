using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.Data.QueryExtensions.Pagination;
using Core.Application.Dto;

namespace Core.Application.Services.Folders
{   
    public interface IFolderService
    {
        Task<List<FolderDto>> ListFoldersForUserAsync(string userId);         
        Task<PagedResult<MovieWithVoteDto>> ListMoviesFromFolderAsync(int folderId, string userId, int page, int pageSize);
        Task<PagedResult<MovieWithGuestVoteDto>> ListMoviesFromFolderForCoupleAsync(int folderId, string ownerId, string guestId, int page, int pageSize);
        Task CreateDefaultFoldersForUserAsync(string userId); 
        Task<int> CreateFolderForUserAsync(string userId, string folderName);
        Task DeleteFolderAsync(string userId, int folderId);
        Task RenameFolderAsync(string userId, int folderId, string newName);
        Task<List<FolderDto>> GetFoldersWithMovieAsync(string userId, int movieId);
        Task AddMovieToFolderAsync(string userId, int folderId, int movieId);
        Task DeleteMovieFromFolderAsync(string userId, int folderId, int movieId);  
    }
}
