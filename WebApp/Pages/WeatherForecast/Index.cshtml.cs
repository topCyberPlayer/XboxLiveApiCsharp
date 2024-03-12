using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.WeatherForecast
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        public IEnumerable<WeatherForecastViewModel>? WeatherForecasts { get; set; }
        public IndexModel(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _client = httpClient;
        }
        public async Task<IActionResult> OnGet()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(_configuration["ConnectionStrings:WeatherForecast"]);
                
                if (response.IsSuccessStatusCode)
                {
                    WeatherForecasts = await response.Content.ReadFromJsonAsync<List<WeatherForecastViewModel>>();
                }

                return Page();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
