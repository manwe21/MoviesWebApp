using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Data.QueryExtensions.Pagination;
using Core.Application.Services.Folders;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Folder;
using Web.Models.Movie;

namespace Web.Pages.User
{
    public class FoldersModel : UserPageModel
    {
        private const int MoviesPageSize = 10;
        
        private readonly IFolderService _folderService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        
        public FolderViewModel CurrentFolder { get; set; }
        
        public PagedResult<MovieWithVoteViewModel> Movies { get; set; }
        public List<FolderViewModel> OwnerFolders { get; set; }
            
        public FoldersModel(IFolderService folderService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _folderService = folderService;
            _userManager = userManager;
            _mapper = mapper;
        }   

        public async Task<IActionResult> OnGetAsync(int? id, [FromQuery(Name = "page_n")] int pageNumber = 1)
        {
            var profileOwner = await _userManager.FindByNameAsync(Username);
            if (profileOwner == null)
                return NotFound();

            var ownerId = profileOwner.Id;

            var folders = await _folderService.ListFoldersForUserAsync(profileOwner.Id);
            OwnerFolders = _mapper.Map<List<FolderViewModel>>(folders.OrderByDescending(f => f.IsDefault));
            
            CurrentFolder = id is null ? OwnerFolders.First() : OwnerFolders.FirstOrDefault(f => f.Id == id);
            if (CurrentFolder == null)
                return NotFound();
            
            switch (RequestInitiator)
            {
                case RequestInitiator.Owner:
                case RequestInitiator.Unknown:
                    var movies = await _folderService.ListMoviesFromFolderAsync
                        (CurrentFolder.Id, ownerId, pageNumber, MoviesPageSize);
                    Movies = _mapper.Map<PagedResult<MovieWithVoteViewModel>>(movies);
                    return Page();
                
                case RequestInitiator.Guest:
                    var guestId = _userManager.GetUserId(User);
                    var result = await _folderService.ListMoviesFromFolderForCoupleAsync
                        (CurrentFolder.Id, ownerId, guestId, pageNumber, MoviesPageSize);
                    Movies = _mapper.Map<PagedResult<MovieWithVoteViewModel>>(result);
                    return Page();
            }
            
            return Page();
        }
    }
}
    