using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Profiles
{
    public class XblLoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public XblLoginModel(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _client = httpClient;
        }
        public async Task<IActionResult> OnGet()
        {
            HttpResponseMessage response = await _client.GetAsync(_configuration["ConnectionStrings:AuthenticationApp"] + "/Authentication/Get");

            if (response.IsSuccessStatusCode)
            {
                string site = await response.Content.ReadAsStringAsync();
                return Redirect(site);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
