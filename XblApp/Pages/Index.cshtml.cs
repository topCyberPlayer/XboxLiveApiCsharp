using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.GamerServices;

namespace XblApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GamerService _gamerService;
        public List<GamerDto> Outputs { get; private set; }

        public IndexModel(GamerService gamerService)
        {
            _gamerService = gamerService;
        }

        public async Task OnGet()
        {
            Outputs = await _gamerService.GetAllGamers().ToListAsync();
        }
    }
}
