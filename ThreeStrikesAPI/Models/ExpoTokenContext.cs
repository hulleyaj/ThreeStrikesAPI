using Microsoft.EntityFrameworkCore;

namespace ThreeStrikesAPI.Models
{
    public class ExpoTokenContext : DbContext
    {
        public ExpoTokenContext(DbContextOptions<ExpoTokenContext> options)
            : base(options)
        {
        }

        public DbSet<ExpoToken> ExpoTokens { get; set; }
    }
}
