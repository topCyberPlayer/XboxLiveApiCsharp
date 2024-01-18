using Microsoft.AspNetCore.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class WeatherForecastController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5256/api");
        private readonly HttpClient _client;
        public WeatherForecastController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<WeatherForecastViewModel>? weatherList = new List<WeatherForecastViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Weatherforecast/Get");

            if (response.IsSuccessStatusCode)
            {
                weatherList = await response.Content.ReadFromJsonAsync<List<WeatherForecastViewModel>>();
            }

            return View(weatherList);
        }
    }
}
