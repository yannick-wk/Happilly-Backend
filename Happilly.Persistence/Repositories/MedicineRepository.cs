using Happilly.Persistence.Abstractions;
using Happilly.Domain.Entities;
using Happilly.Persistence.Database;
using Microsoft.Extensions.Logging;

namespace Happilly.Persistence.Repositories
{
    public class MedicineRepository : RepositoryBase<Medicine, HappillyDbContext>
    {
        public MedicineRepository(HappillyDbContext dbContext, ILogger<HappillyDbContext> logger) : base(dbContext, logger)
        {
        }
    }
}