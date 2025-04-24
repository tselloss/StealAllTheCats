using CatsLibrary.Interface;
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


        [HttpPost("fetch")]
        public async Task<IActionResult> FetchAndSaveCats()
        {
            await _catsInterface.FetchAndSaveTheCats();

            return Ok();
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetCats(int id)
        {
            var catById = _catsInterface.GetCatById(id);

            if (catById == null)
            {
                return NotFound(); 
            }

            return Ok(catById);
        }
    }


}

