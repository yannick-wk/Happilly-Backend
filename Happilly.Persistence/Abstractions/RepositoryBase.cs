using Happilly.Domain.Entities;
using Happilly.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Happilly.Persistence.Database;
using System.Linq.Expressions;

namespace Happilly.Persistence.Abstractions 
{
    /// <summary>
    /// Represents the <see cref="RepositoryBase{TEntity, TDbContext}"/> class.
    /// A base implementation of a Repository pattern using the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> from EntityFramework.
    /// </summary>
    /// <typeparam name="TEntity">The entity the repository pattern is used with.</typeparam>
    /// <typeparam name="TDbContext">The DbContext the repository pattern is used with.</typeparam>
    public abstract class RepositoryBase<TEntity, TDbContext> : IRepository<TEntity> where TDbContext : DbContext where TEntity : class, IEntity
    {
        protected readonly TDbContext DbContext;
        protected readonly ILogger<TDbContext> Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TEntity, TDbContext}"/> class.
        /// </summary>
        /// <param name="dbContext">The DbContext.</param>
        /// <param name="logger">The logger.</param>
        protected RepositoryBase(TDbContext dbContext, ILogger<TDbContext> logger)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc cref="IRepository{TEntity}.GetPaginationAsync(int, int)"/>
        public virtual async Task<IEnumerable<TEntity>> GetPaginationAsync(int pageNumber, int pageSize)
        {
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            return await DbContext.Set<TEntity>().Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
        }

        /// <inheritdoc cref="IRepository{TEntity}.ExistsAsync(Expression{Func{TEntity, bool}})"/>
        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            return await DbContext.Set<TEntity>().AnyAsync(predicate);
        }
        
        /// <inheritdoc cref="IRepository{TEntity}.ExistsAsync(Expression{Func{TEntity, bool}})"/>
        public async Task<bool> SaveChangesAsync()
        {
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            bool saveSuccess = false;
            try
            {
                int saveResult = await DbContext.SaveChangesAsync();
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception ex)
            {
                Exception savingChangesFailedException = new Exception("The changes failed to save.", ex);
                Logger.LogError(savingChangesFailedException, savingChangesFailedException.Message);
                throw savingChangesFailedException;
            }
            return saveSuccess;
        }

        /// <inheritdoc cref="IRepository{TEntity}.FindManyAsync(Expression{Func{TEntity, bool}})"/>
        public virtual async Task<IEnumerable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            return await DbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        /// <inheritdoc cref="IRepository{TEntity}.FindSingleOrDefaultAsync(Expression{Func{TEntity, bool}})"/>
        public virtual async Task<TEntity> FindSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            return await DbContext.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        /// <inheritdoc cref="IRepository{TEntity}.GetAllAsync()"/>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            return await DbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            DbContext.Set<TEntity>().Add(entity);
            int changes = 0;
            try
            {
                changes = await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return changes > 0; 
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            TEntity obj = DbContext.Set<TEntity>().Find(id);
            if (obj == null) 
            { 
                return false; 
            }

            DbContext.Set<TEntity>().Remove(obj);

            int changes = 0;
            try
            {
                changes = await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return changes > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
        
            TEntity obj = DbContext.Set<TEntity>().Find(entity.Id);
            if (obj == null)
            {
                return false;
            }
            DbContext.Entry(obj).CurrentValues.SetValues(entity);

            int changes = 0;
            try
            {
                changes = await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return changes > 0;
        }
    }
}