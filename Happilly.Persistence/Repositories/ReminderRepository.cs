using Happilly.Persistence.Abstractions;
using Happilly.Domain.Entities;
using Happilly.Persistence.Database;
using Microsoft.Extensions.Logging;

namespace Happilly.Persistence.Repositories
{
    public class ReminderRepository : RepositoryBase<Reminder, HappillyDbContext>
    {
        public ReminderRepository(HappillyDbContext dbContext, ILogger<HappillyDbContext> logger) : base(dbContext, logger)
        {
        }
    }
}