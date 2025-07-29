using Microsoft.AspNetCore.Mvc;
using XblApp.Application.XboxLiveUseCases;
using XblApp.Domain.DTO;
using XblApp.Domain.Entities;

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
            List<Game> result = await useCase.GetGamesAsync();

            if (result is null || result.Count == 0)
                return NotFound("Игры не найдены");

            List<GameDTO> result2 = GameDTO.CastToGameDTO(result).ToList();
            return Ok(result2);
        }

        // GET api/<GameController>/5
        [HttpGet("{gameId}")]
        public async Task<ActionResult<GameDTO>> Get(long gameId)
        {
            Game result = await useCase.GetGameAsync(gameId);

            if (result is null)
                return NotFound("Игра не найдена");

            GameDTO? result2 = GameDTO.CastToGameDTO(result);
            return Ok(result2);
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
