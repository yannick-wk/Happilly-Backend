using Microsoft.EntityFrameworkCore;

namespace Happilly.Persistence.Database
{
    public class HappillyDbContext : DbContext
    {
        public HappillyDbContext(DbContextOptions<HappillyDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Applies all entity configurations.
        /// </summary>
        /// <param name="modelBuilder">the model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations<HappillyDbContext>();
            base.OnModelCreating(modelBuilder);
        }
    }
}