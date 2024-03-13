using Microsoft.AspNetCore.Mvc;
using ProfileService.Services;
using System.ComponentModel.DataAnnotations;


namespace ProfileService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProfileController : ControllerBase
    {
        private ProfileServiceXbl _profileService;

        private readonly List<string> _fruit = new List<string> { "Pear", "Lemon", "Peach" };

        public ProfileController(ProfileServiceXbl profileService)
        {
            _profileService = profileService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> GetProfileByGamertag(InputModel model)
        {
            if(ModelState.IsValid)
            {
                HttpResponseMessage response = await _profileService.GetProfileByGamertag(model.Gamertag, model.AuthorizationCode);
                
                if (response.IsSuccessStatusCode) 
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return responseData;
                }
                else 
                {
                    return BadRequest($"Ошибка при получении информации из Xbox Live. Причина: {response.ReasonPhrase}");
                }
            }
            return BadRequest();
        }

        [HttpGet]
        //[HttpGet(Name = "GetWeather")] - Это Conventional routing (маршрутизация на основе соглашений)
        public ActionResult<string> GetProfileBy(int id)
        {
            //Query string values: http://localhost:5245/api/Profile/GetProfileBy?id=2

            //ActionResult<ProfileModelXbl> profile = await _profileService.GetProfileByGamertag(gamertag);

            if (id >= 0 && id < _fruit.Count)
            {
                return _fruit[id];
            }

            return NotFound();
        }

        [HttpGet("fruit/{id}")]
        public ActionResult<string> View(int id)
        {
            //Route values (path в Postman): http://localhost:5245/api/Profile/View/fruit/2
            if (id >= 0 && id < _fruit.Count)
            {
                return _fruit[id];
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Vieww(int id)
        {
            //Route values (path в Postman): http://localhost:5245/api/Profile/Vieww/2
            if (id >= 0 && id < _fruit.Count)
            {
                return _fruit[id];
            }

            return NotFound();
        }
    }

    public class InputModel
    {
        [Required]
        public string? Gamertag { get; set; }

        [Required]
        public string? AuthorizationCode { get; set; }
    }
}
