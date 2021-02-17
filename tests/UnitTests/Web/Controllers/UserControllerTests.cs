using System.Threading.Tasks;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Controllers;
using Xunit;

namespace UnitTests.Web.Controllers
{
    public class UserControllerTests : TestBase
    {
        private readonly UserManager<AppUser> _userManager;
        
        public UserControllerTests()
        {
            var userStore = Mock.Of<IUserStore<AppUser>>();
            var userManager = new Mock<UserManager<AppUser>>(userStore, null, null, null, null, null, null, null, null);
            userManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null);
            _userManager = userManager.Object;
        }
        
        [Fact]
        public async Task GetGenres_UserNotFound_404Returned()
        {
            var controller = new UserController(null, _userManager);
            var res = await controller.GetGenres("name");

            Assert.IsType<NotFoundResult>(res);
        }

        [Fact]
        public async Task GetActors_UserNotFound_404Returned()
        {
            var controller = new UserController(null, _userManager);
            var res = await controller.GetActors("name");

            Assert.IsType<NotFoundResult>(res);
        }
        
        [Fact]
        public async Task GetYears_UserNotFound_404Returned()
        {
            var controller = new UserController(null, _userManager);
            var res = await controller.GetYears("name");

            Assert.IsType<NotFoundResult>(res);
        }
    }
}