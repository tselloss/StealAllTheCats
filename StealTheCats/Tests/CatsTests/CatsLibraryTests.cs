using CatsLibrary.Controller;
using CatsLibrary.Interface;
using DatabaseContext.DBHelper.DTO;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace CatsLibrary.Tests
{
    public class CatsControllerTests
    {
        private ICatsInterface _catsInterface;
        private CatsController _catsController;

        [OneTimeSetUp]
        public void Setup()
        {
            _catsInterface = A.Fake<ICatsInterface>();
            _catsController = new CatsController(_catsInterface);
        }

        [Test]
        public async Task CatsController_FetchAndSaveCats_Return_OK()
        {
            // Arrange
            A.CallTo(() => _catsInterface.FetchAndSaveTheCats()).DoesNothing();

            // Act
            var result = await _catsController.FetchAndSaveCats();

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public async Task CatsController_GetCatsById_Return_CatInfo_Success()
        {
            // Arrange
            var catId = 1;
            var expectedCat = new CatEntityDto
            {
                Id = 1,
                Width = 500,
                Height = 400,
                Url = "https://example.com/cat.jpg"
            };
            A.CallTo(() => _catsInterface.GetCatById(catId)).Returns(expectedCat);

            // Act
            var result = await _catsController.GetCats(catId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var returnCat = okResult.Value as CatEntityDto;
            returnCat.Should().BeEquivalentTo(expectedCat);
        }

        [Test]
        public async Task CatsController_GetCatsById_Return_NotFound_When_CatDoesNotExist()
        {
            // Arrange
            var catId = 999;
            var expectedResult = (CatEntityDto)null;
            A.CallTo(() => _catsInterface.GetCatById(catId)).Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _catsController.GetCats(catId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public void CatsController_GetCatsByPage_Return_CatsList_Success()
        {
            // Arrange
            var catsList = new List<CatEntityDto>
            {
                new CatEntityDto { Id = 1, Url = "https://cat1.jpg", Width = 500, Height = 400 },
                new CatEntityDto { Id = 2, Url = "https://cat2.jpg", Width = 600, Height = 500 }
            };
            var page = 1;
            var pageSize = 10;
            A.CallTo(() => _catsInterface.GetCatsByPages(page, pageSize)).Returns(catsList);

            // Act
            var result = _catsController.GetCatsByPage(page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(catsList);
        }

        [Test]
        public async Task CatsController_GetCatsByTag_Return_CatsList_Success()
        {
            // Arrange
            var catsList = new List<CatEntityDto>
            {
                new CatEntityDto { Id = 1, Url = "https://cat1.jpg", Width = 500, Height = 400 },
                new CatEntityDto { Id = 2, Url = "https://cat2.jpg", Width = 600, Height = 500 }
            };
            var tag = "playful";
            var page = 1;
            var pageSize = 10;
            A.CallTo(() => _catsInterface.GetCatsByPages(page, pageSize, tag)).Returns(catsList);

            // Act
            var result = await _catsController.GetCatsByTag(page, pageSize, tag);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var returnCats = okResult.Value as IEnumerable<CatEntityDto>;
            returnCats.Should().BeEquivalentTo(catsList);
        }

        [Test]
        public async Task CatsController_GetCatsByTag_Return_NotFound_When_NoCatsForTag()
        {
            // Arrange
            var tag = "playful";
            var page = 1;
            var pageSize = 10;
            A.CallTo(() => _catsInterface.GetCatsByPages(page, pageSize, tag)).Returns(new List<CatEntityDto>());

            // Act
            var result = await _catsController.GetCatsByTag(page, pageSize, tag);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
