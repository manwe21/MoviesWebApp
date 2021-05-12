using System.Linq;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Data.QueryExtensions.Folders
{
    public static class FolderQueries
    {
        public static IQueryable<Folder> GetFoldersWithMovie(this IQueryable<Folder> source, string userId, int movieId)
        {
            return source
                .Where(f => f.OwnerId == userId)
                .Where(f => f.MovieFolders.Any(mf => mf.MovieId == movieId));
        }

        public static bool IsFolderWithNameAlreadyExists(this IQueryable<Folder> source, string userId, FolderName folderName)
        {
            return source.Any(f => f.OwnerId == userId && f.Name.Value == folderName.Value);
        }
    }
}
