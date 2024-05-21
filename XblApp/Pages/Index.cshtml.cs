using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.GamerServices;
using ServiceLayer.Models;

namespace XblApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GamerService _gamerService;
        public List<GamerModelDto> Outputs { get; private set; }

        public IndexModel(GamerService gamerService)
        {
            _gamerService = gamerService;
        }

        public void OnGet()
        {
            Outputs = _gamerService.GetAllGamers().ToList();
        }
    }
}
