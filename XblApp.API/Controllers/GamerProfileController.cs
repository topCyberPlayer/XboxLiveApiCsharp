using Microsoft.AspNetCore.Mvc;
using XblApp.Application;
using XblApp.DTO;

namespace XblApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamerProfileController : ControllerBase
    {
        private GamerProfileUseCase _gamerProfileUC;

        public GamerProfileController(GamerProfileUseCase gamerProfileUC)
        {
            _gamerProfileUC = gamerProfileUC;
        }
        
        [HttpGet("[action]")]
        public async Task<IEnumerable<GamerDTO>> GetGamerProfiles()
        {
            var result = await _gamerProfileUC.GetAllGamerProfilesRepoAsync();
            var result2 = GamerDTO.CastToToGamerDTO(result);

            return result2;
        }
    }
}
