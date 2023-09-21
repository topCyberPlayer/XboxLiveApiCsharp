using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.Profiles
{
    public class CurrentProfileModel : PageModel
    {
        private readonly WebAppContext _context;
        public IList<ProfileUser> ProfileUserList { get; set; } = default!;

        public CurrentProfileModel(WebAppContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            if (_context.ProfileUser != null)
            {
                ProfileUserList = await _context.ProfileUser.ToListAsync();
            }
        }
    }
}
