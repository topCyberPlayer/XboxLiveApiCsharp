using Microsoft.AspNetCore.Mvc;
using XblApp.Application.XboxLiveUseCases;
using XblApp.Domain.DTO;
using XblApp.Domain.Entities;

namespace XblApp.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class GamerController(GamerProfileUseCase useCase) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GamerDTO>>> GetGamerProfiles()
        {
            IEnumerable<GamerDTO> result = await useCase.GetAllGamerProfilesRepoAsync();
            return Ok(result);
        }

        [HttpGet("{gamertag}")]
        public async Task<ActionResult<GamerDTO>> GetGamerProfiles(string gamertag)
        {
            GamerDTO result = await useCase.GetGamerProfileRepoAsync(gamertag);
            return Ok(result);
        }
    }
}
