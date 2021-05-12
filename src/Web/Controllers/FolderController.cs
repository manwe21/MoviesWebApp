using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dto;
using Application.Services.Folders;
using AutoMapper;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Filters;
using Web.Models.Folder;

namespace Web.Controllers
{
    [AjaxAuthentication]
    public class FolderController : BaseController
    {
        private readonly IFolderService _folderService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<FolderController> _logger;
        private readonly IMapper _mapper;

        public FolderController(IFolderService folderService, UserManager<AppUser> userManager, ILogger<FolderController> logger, IMapper mapper)
        {
            _folderService = folderService;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> Create(string folderName)
        {
            var userId = _userManager.GetUserId(User);
            int folderId = await _folderService.CreateFolderForUserAsync(userId, folderName);
            return Json(new FolderViewModel { Id = folderId, Name = folderName });
        }

        [HttpPost("rename")]
        public async Task<IActionResult> Rename(int folderId, string newName)
        {
            var userId = _userManager.GetUserId(User);
            await _folderService.RenameFolderAsync(userId, folderId, newName);
            return Json(new FolderViewModel { Id = folderId, Name = newName });
        }
        
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int folderId)
        {
            var userId = _userManager.GetUserId(User);
            await _folderService.DeleteFolderAsync(userId, folderId);
            return Ok();    
        }
        
        [HttpGet("movie/lists-data")]
        public async Task<IActionResult> GetFoldersDataForMovie(int movieId)
        {
            var userId = _userManager.GetUserId(User);
            var folders = await _folderService.ListFoldersForUserAsync(userId);
            var foldersWithMovie = await _folderService.GetFoldersWithMovieAsync(userId, movieId);
            
            var foldersModel = _mapper.Map<List<FolderDto>, List<FolderViewModel>>(folders);
            var idList = foldersWithMovie.Select(f => f.Id).ToList();
            return Json(new FoldersMenuViewModel { Lists = foldersModel, ListsWithMovie = idList });
        }
        
        [HttpPost("movies/remove")]
        public async Task<IActionResult> RemoveMovie(int folderId, int movieId)
        {
            var userId = _userManager.GetUserId(User);
            await _folderService.DeleteMovieFromFolderAsync(userId, folderId, movieId);
            return Ok();
        }
        
        [HttpPost("movies/add")]
        public async Task<IActionResult> AddMovie(int folderId, int movieId)
        {
            var userId = _userManager.GetUserId(User);
            await _folderService.AddMovieToFolderAsync(userId, folderId, movieId);
            return Ok();
        }
        
    }
}
