using Microsoft.EntityFrameworkCore;

namespace ThreeStrikesAPI.Models
{
    public class ThreeStrikesItemContext : DbContext
    {
        public ThreeStrikesItemContext(DbContextOptions<ThreeStrikesItemContext> options)
            : base(options)
        {
        }

        public DbSet<ThreeStrikesItem> ThreeStrikesItems { get; set; }
    }
}
