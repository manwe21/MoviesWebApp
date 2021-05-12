using System.Threading.Tasks;
using Application.Services.Credits;
using Xunit;

namespace IntegrationTests.Services.Credits
{
    public class CreditsServiceTests : TestBase
    {
        [Fact]
        public async Task GetPersonFilmography_CorrectDataReturned()
        {
            var db = CreateAndSeedDb();
            var service = new CreditsService(db);

            var filmography = await service.GetPersonFilmographyAsync(1);
            
            Assert.Equal(4, filmography.Count);
            
            Assert.Equal(3, filmography[0].MovieId);
            Assert.Equal("Acting", filmography[0].Department);
            
            Assert.Equal(3, filmography[1].MovieId);
            Assert.Equal("Directing", filmography[1].Department);
            
            Assert.Equal(1, filmography[2].MovieId);
            Assert.Equal("Acting", filmography[2].Department);
            
            Assert.Equal(2, filmography[3].MovieId);
            Assert.Equal("Camera", filmography[3].Department);
        }
        
    }
}
