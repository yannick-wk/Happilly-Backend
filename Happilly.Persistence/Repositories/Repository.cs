using Happilly.Domain.Entities;
using Happilly.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Happilly.Persistence.Repositories
{
    /// <summary>
    /// Represents the <see cref="Repository{TEntity, TDbContext}"/> class.
    /// The default implementation of the <see cref="RepositoryBase{TEntity, TDbContext}"/> class.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TDbContext">The database context type.</typeparam>
    public sealed class Repository<TEntity, TDbContext> : RepositoryBase<TEntity, TDbContext> where TEntity : class, IEntity where TDbContext : DbContext
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Repository{TEntity, TDbContext}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="logger">The logger.</param>
        public Repository(TDbContext dbContext, ILogger<TDbContext> logger) : base(dbContext, logger)
        {

        }
    }
}