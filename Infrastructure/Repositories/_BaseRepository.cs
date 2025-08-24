using XblApp.Infrastructure.Contexts;

namespace XblApp.Infrastructure.Repositories
{
    public class BaseRepository
    {
        internal readonly ApplicationDbContext context;

        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
    }
}
