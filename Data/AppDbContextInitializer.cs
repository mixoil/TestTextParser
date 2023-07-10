using Microsoft.EntityFrameworkCore;
using TestTextParser.Data;

namespace TestKSK
{
    public class AppDbContextInitializer
    {
        private readonly AppDbContext appDbContext;

        public AppDbContextInitializer()
        {
            appDbContext = new AppDbContext();
        }

        public async Task Migrate(CancellationToken cancellationToken = default)
        {
            await appDbContext.Database.MigrateAsync(cancellationToken);
        }
    }
}
