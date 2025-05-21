using Microsoft.AspNetCore.Mvc;
using XblApp.Application;
using XblApp.Domain.Entities;
using XblApp.DTO;

namespace XblApp.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class GamerController(GamerProfileUseCase useCase) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GamerDTO>>> GetGamerProfiles()
        {
            List<Gamer> result = await useCase.GetAllGamerProfilesRepoAsync();
            if (result is null || result.Count == 0)
                return NotFound("Игроки не найдены");

            List<GamerDTO> result2 = GamerDTO.CastToGamerDTO(result).ToList();
            return Ok(result2);
        }

        [HttpGet]
        [HttpGet("{gamertag}")]
        public async Task<ActionResult<GamerDTO>> GetGamerProfiles(string gamertag)
        {
            Gamer result = await useCase.GetGamerProfileRepoAsync(gamertag);
            if (result is null)
                return NotFound("Игрок не найден");

            GamerDTO? result2 = GamerDTO.CastToGamerDTO(result);
            return Ok(result2);
        }
    }
}
