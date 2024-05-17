using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.GamerServices;
using ServiceLayer.Models;

namespace XblApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GamerServiceDb _gamerService;
        public List<GamerModelDto> Outputs { get; private set; }

        public IndexModel(GamerServiceDb gamerService)
        {
            _gamerService = gamerService;
        }

        public async Task OnGet()
        {
            Outputs = await _gamerService.GetAllGamers().ToListAsync();
        }
    }
}
