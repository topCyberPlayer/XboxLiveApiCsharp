using XblApp.Database.Contexts;

namespace XblApp.Database.Repositories
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
