using Happilly.Application.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Happilly.Persistence.Database
{
    /// <summary>
    /// Represents the <see cref="HappillyDatabaseFactory"/> class.
    /// </summary>
    public class HappillyDatabaseFactory : DatabaseFactory<HappillyDbContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HappillyDatabaseFactory"/> class.
        /// </summary>
        public HappillyDatabaseFactory() : base(null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HappillyDatabaseFactory"/> class.
        /// </summary>
        /// <param name="dbConfigurationOptions">The options.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public HappillyDatabaseFactory(IOptions<DbConfiguration> dbConfigurationOptions, ILoggerFactory loggerFactory) : base(dbConfigurationOptions, loggerFactory)
        {

        }

        /// <summary>
        /// Creates a new instance of the <see cref="HappillyDbContext"/> class.
        /// </summary>
        /// <param name="dbContextOptions">The options.</param>
        /// <returns>The user database context.</returns>
        protected override HappillyDbContext CreateNewInstance(DbContextOptions<HappillyDbContext> dbContextOptions)
        {
            return new HappillyDbContext(dbContextOptions);
        }
    }
}
