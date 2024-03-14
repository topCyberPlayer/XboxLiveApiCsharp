using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Profile
{
    public class ProfileModel : PageModel
    {
        private readonly string _weatherForecastUrl;
        private readonly IHttpClientFactory _httpClientFactory;
        //public IEnumerable<WeatherForecastViewModel>? WeatherForecasts { get; set; }
        public ProfileModel(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _weatherForecastUrl = configuration["ConnectionStrings:WeatherForecast"];
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGet()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await httpClient.GetAsync(_weatherForecastUrl+"/api/Weatherforecast/Get");

            //if (response.IsSuccessStatusCode)
            //{
            //    //WeatherForecasts = await response.Content.ReadFromJsonAsync<List<WeatherForecastViewModel>>();
            //}

            return Page();
        }
    }
}
