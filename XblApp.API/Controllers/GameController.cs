using Application.XboxLiveUseCases;
using Microsoft.AspNetCore.Mvc;
using Domain.DTO;
using Domain.Requests;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XblApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(GameUseCase useCase) : ControllerBase
    {

        // GET: api/<GameController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> Get()
        {
            IEnumerable<GameDTO> result = await useCase.GetGamesAsync();
            return Ok(result);
        }

        // GET api/<GameController>/5
        [HttpGet("{gameId:long}")]
        public async Task<ActionResult<GameDTO>> GetById(long gameId)
        {
            GameDTO result = await useCase.GetGameAsync(gameId);
            return Ok(result);
        }

        // POST api/<GameController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameRequest request)
        {
            await useCase.AddGameAsync(request);
            return Ok();
        }

        // PUT api/<GameController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
