using Forum.bg.Data.Configure;
using Forum.bg.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.bg.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PostConfiguration());

            base.OnModelCreating(builder);
        }

        public virtual DbSet<Post> Posts { get; init; } = null!;
    }
}
