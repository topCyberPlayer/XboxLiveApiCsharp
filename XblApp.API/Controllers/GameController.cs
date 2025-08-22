using Microsoft.AspNetCore.Mvc;
using XblApp.Application.XboxLiveUseCases;
using XblApp.Domain.DTO;

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
        public void Post([FromBody] string value)
        {
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
