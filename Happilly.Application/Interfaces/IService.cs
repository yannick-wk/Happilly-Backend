namespace Happilly.Application.Interfaces
{
    /// <summary>
    /// Represents the <see cref="IService{TDto}"/> interface.
    /// </summary>
    public interface IService<TDto> where TDto : class
    {
        /// <summary>
        /// Finds a single dto by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns the dto by id.</returns>
        Task<TDto> FindSingleOrDefaultAsync(Guid id);

        /// <summary>
        /// Gets all records as dtos.
        /// </summary>
        /// <returns>Returns all dtos.</returns>
        Task<IEnumerable<TDto>> GetAllAsync();
        
        /// <summary>
        /// Gets paginated records as dtos.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>Returns all paginated dtos.</returns>
        Task<IEnumerable<TDto>> GetPaginationAsync(int pageNumber, int pageSize);
    }
}