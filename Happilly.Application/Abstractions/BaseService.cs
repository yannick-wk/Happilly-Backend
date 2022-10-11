using Happilly.Domain.Entities;
using Happilly.Application.Interfaces;
using AutoMapper;

namespace Happilly.Application.Abstractions
{
    /// <summary>
    /// Represents the <see cref="BaseService{TEntity,TDto}"/> class.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TDto">The dto type.</typeparam>
    public abstract class BaseService<TEntity, TDto> : IService<TDto> where TDto : class where TEntity : class, IEntity
    {
        protected readonly IMapper Mapper;
        protected readonly IRepository<TEntity> EntityRepository;

        /// <summary>
        /// Initializes an instance of the <see cref="BaseService{TEntity,TDto}"/> class.
        /// </summary>
        /// <param name="entityRepository">The entity repository.</param>
        /// <param name="mapper">The mapper.</param>
        protected BaseService(IRepository<TEntity> entityRepository, IMapper mapper)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(entityRepository));
            EntityRepository = entityRepository ?? throw new ArgumentNullException(nameof(mapper));
        }

        public virtual Task<bool> CreateAsync(TDto dto)
        {
            // #1 Map DTO -> Entity
            TEntity entity = Mapper.Map<TEntity>(dto);
            return EntityRepository.CreateAsync(entity);
        }

        /// <inheritdoc cref="IService{TEntity}.FindSingleOrDefaultAsync(Guid)"/>
        public virtual Task<TDto> FindSingleOrDefaultAsync(Guid id)
        {
            return EntityRepository.FindSingleOrDefaultAsync(tr => tr.Id == id)
                .ContinueWith(entity => Mapper.Map<TDto>(entity.Result));
        }

        /// <inheritdoc cref="IService{TEntity}.GetAllAsync()"/>
        public virtual Task<IEnumerable<TDto>> GetAllAsync()
        {
            return EntityRepository.GetAllAsync()
                .ContinueWith(entities => Mapper.Map<IEnumerable<TDto>>(entities.Result));
        }

        /// <inheritdoc cref="IService{TEntity}.GetPaginationAsync(int, int)"/>
        public virtual Task<IEnumerable<TDto>> GetPaginationAsync(int pageNumber, int pageSize)
        {
            return EntityRepository.GetPaginationAsync(pageNumber, pageSize)
                .ContinueWith(entities => Mapper.Map<IEnumerable<TDto>>(entities.Result));
        }
    }
}