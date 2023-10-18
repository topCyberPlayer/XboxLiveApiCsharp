using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.ModelsDb;

namespace WebApp.Pages.Movies
{
    public class DeleteModel : PageModel
    {
        private readonly WebAppDbContext _context;

        public DeleteModel(WebAppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }
            else 
            {
                Movie = movie;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.FindAsync(id);

            if (movie != null)
            {
                Movie = movie;
                _context.Movies.Remove(Movie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
