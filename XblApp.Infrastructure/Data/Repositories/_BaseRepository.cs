using Microsoft.AspNetCore.Http;

namespace XblApp.Infrastructure.Data.Repositories
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
