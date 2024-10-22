using Microsoft.AspNetCore.Http;

namespace XblApp.Infrastructure.Data.Repositories
{
    public class BaseService
    {
        internal readonly XblAppDbContext _context;
        
        public BaseService(XblAppDbContext context)
        {
            _context = context;
        }
    }
}
