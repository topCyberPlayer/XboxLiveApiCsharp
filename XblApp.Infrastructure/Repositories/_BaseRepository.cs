using Microsoft.AspNetCore.Http;
using XblApp.Database.Contexts;

namespace XblApp.Database.Repositories
{
    public class BaseRepository
    {
        internal readonly XblAppDbContext _context;

        public BaseRepository(XblAppDbContext context)
        {
            _context = context;
        }
    }
}
