using Happilly.Domain.Entities;
using System.Linq.Expressions;

namespace Happilly.Application.Interfaces
{
    /// <summary>
    /// Represents the <see cref="IRepository{TEntity}"/> interface for generic repositories.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Finds a single Entity by an expression asynchronously.
        /// </summary>
        /// <param name="predicate">The expression.</param>
        /// <returns>Returns the result of the expression.</returns>
        Task<TEntity> FindSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Finds many Entities using an expression asynchronously.
        /// </summary>
        /// <param name="predicate">The expression.</param>
        /// <returns>The results of the expression.</returns>
        Task<IEnumerable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets all Entities asynchronously.
        /// </summary>
        /// <returns>Returns all Entities.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<bool> CreateAsync(TEntity entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Gets pagination asynchronously.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>Returns all paginated entities.</returns>
        Task<IEnumerable<TEntity>> GetPaginationAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Determines whether a predicate exists asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Returns <c>true</c> If the predicate returns successful; otherwise, <c>false</c>.</returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Saves changes asynchronously.
        /// </summary>
        /// <exception cref="SavingChangesFailedException">If it fails to the save changes.</exception>
        /// <returns>Returns <c>true</c> If the changes are successful; otherwise, <c>false</c>.</returns>
        Task<bool> SaveChangesAsync();
    }
}