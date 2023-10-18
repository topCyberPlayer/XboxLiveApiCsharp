using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.ModelsDb;

namespace WebApp.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly WebAppDbContext _context;
        public IList<Movie> Movie { get; set; } = default!;

        public IndexModel(WebAppDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            if (_context.Movies != null)
            {
                Movie = await _context.Movies.ToListAsync();
            }
        }
    }
}
