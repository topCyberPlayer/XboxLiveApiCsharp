using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Profile;

namespace WebApp.Pages.Profiles
{
    public class AllProfileUsersModel : PageModel
    {
        private readonly WebAppDbContext _context;
        public IList<ProfileUserModelDb> ProfileUserList { get; set; } = default!;

        public AllProfileUsersModel(WebAppDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            if (_context.ProfileUsers!= null)
            {
                ProfileUserList = await _context.ProfileUsers.ToListAsync();
            }
        }
    }
}
