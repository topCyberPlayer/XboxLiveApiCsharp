using XblApp.Infrastructure.Contexts;

namespace XblApp.Infrastructure.Repositories
{
    public class BaseRepository
    {
        internal readonly XblAppDbContext context;

        public BaseRepository(XblAppDbContext context)
        {
            this.context = context;
        }
    }
}
