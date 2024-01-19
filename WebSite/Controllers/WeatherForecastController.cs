using Microsoft.AspNetCore.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        public WeatherForecastController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _client = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<WeatherForecastViewModel>? weatherList = new List<WeatherForecastViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_configuration["ConnectionStrings:WeatherForecast"] + "/Weatherforecast/Get");

            if (response.IsSuccessStatusCode)
            {
                weatherList = await response.Content.ReadFromJsonAsync<List<WeatherForecastViewModel>>();
            }

            return View(weatherList);
        }
    }
}
