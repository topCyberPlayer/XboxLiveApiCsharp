using Microsoft.AspNetCore.Mvc;
using ProfileService.Profiles;
using ProfileService.Services;
using System.ComponentModel.DataAnnotations;
using DomainModel.Profiles;


namespace ProfileService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProfileController : ControllerBase
    {
        private ProfileServiceR service;

        private readonly List<string> _fruit = new List<string> { "Pear", "Lemon", "Peach" };

        public ProfileController(ProfileServiceR profileService)
        {
            service = profileService;
        }

        [HttpGet]
        public async Task<ActionResult<ProfileModelDTO>> GetProfileByGamertag(string gamertag)
        {
            string authorizationHeader = Request.Headers.Authorization;

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return BadRequest("Authorization headers are required.");
            }

            ProfileModelDTO profile = await service.GetProfileByGamertag(gamertag, authorizationHeader);

            return profile != null ? Ok(profile) : NotFound("Gamertag не найден");
        }

        [HttpGet]
        public ActionResult<ProfileModelDb> GetProfileByGamertagTest(string gamertag)
        {
            ProfileModelDb profile = service.GetProfileByGamertagTest(gamertag);

            return profile != null ? Ok(profile) : NotFound();
        }

        [HttpGet]
        public ActionResult<string> GetProfileBy(int id)
        {
            //Query string values: http://localhost:5245/api/Profile/GetProfileBy?id=2

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
