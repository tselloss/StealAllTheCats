using CatsLibrary.Interface;
using DatabaseContext.DBHelper.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CatsLibrary.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatsController : ControllerBase
    {
        private readonly ICatsInterface _catsInterface;

        public CatsController(ICatsInterface catsInterface)
        {
            _catsInterface = catsInterface;
        }

        /// <summary>
        /// Fetch Cats From CatsAPI and Save them to the local SQL DB
        /// </summary>
        /// <returns>OK</returns>
        /// /// <returns>Fetch and Save</returns>
        /// <response code="200">Request Successfull</response>
        /// <response code="404">If no cats are found from the API</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPost("fetch")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<IActionResult> FetchAndSaveCats()
        {
            await _catsInterface.FetchAndSaveTheCats();

            return Ok();
        }

        /// <summary>
        /// Retrieve the Cats from the DB by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <returns>Get Cats by ID</returns>
        /// <response code="200">Request Successfull</response>
        /// <response code="404">If no cats are found in the db with this ID.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("/{id}")]
        [ProducesResponseType(typeof(CatEntityDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<IActionResult> GetCats(int id)
        {
            var catById = await _catsInterface.GetCatById(id);

            if (catById == null)
            {
                return NotFound();
            }

            return Ok(catById);
        }

        /// <summary>
        /// Get the cats by Page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <returns>List of Cats by Page</returns>
        /// <response code="200">Request Successfull</response>
        /// <response code="404">If no cats around.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("page")]
        [ProducesResponseType(typeof(IEnumerable<CatEntityDto>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public IEnumerable<CatEntityDto> GetCatsByPage(int page = 1, int pageSize = 10)
        {
            var catsByPage = _catsInterface.GetCatsByPages(page, pageSize);

            return catsByPage;
        }

        /// <summary>
        /// Get the cats by tag
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="tag"></param>
        /// <returns>List of Cats by Tag</returns>
        /// <response code="200">Request Successfull</response>
        /// <response code="404">If no cats are found for the specified tag.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("tag")]
        [ProducesResponseType(typeof(IEnumerable<CatEntityDto>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<IActionResult> GetCatsByTag([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string tag)
        {
            var cats = await _catsInterface.GetCatsByPages(page, pageSize, tag);

            if (cats == null || !cats.Any())
            {
                return NotFound($"No cats found with tag '{tag}'.");
            }

            return Ok(cats);
        }
    }
}

