using Application.XboxLiveUseCases;
using Microsoft.AspNetCore.Mvc;
using XblApp.Domain.DTO;

namespace XblApp.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GamerController(GamerProfileUseCase useCase) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GamerDTO>>> GetGamerProfiles()
        {
            IEnumerable<GamerDTO> result = await useCase.GetAllGamerProfilesAsync();
            return Ok(result);
        }

        [HttpGet("{gamertag}")]
        public async Task<ActionResult<GamerDTO>> GetGamerProfiles(string gamertag)
        {
            GamerDTO result = await useCase.GetGamerProfileAsync(gamertag);
            return Ok(result);
        }
    }
}
