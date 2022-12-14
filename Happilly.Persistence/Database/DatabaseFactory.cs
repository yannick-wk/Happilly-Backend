using Happilly.Application.Configurations;
using Happilly.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happilly.Persistence.Database
{
    /// <summary>
    /// Represents the <see cref="DatabaseFactory{TDbContext}"/> class; an implementation of the abstract <see cref="DesignTimeDbContextFactoryBase{TContext}"/> class.
    /// </summary>
    public abstract class DatabaseFactory<TDbContext> : DesignTimeDbContextFactoryBase<TDbContext>, IFactory<TDbContext> where TDbContext : DbContext
    {
        private readonly string[] _arguments = { "" };

        /// <summary>
        /// Initializes in instance of the <see cref="DatabaseFactory{TDbContext}"/> class.
        /// </summary>
        protected DatabaseFactory() : base(null, null)
        {

        }

        /// <summary>
        /// Initializes an instance of the <see cref="DatabaseFactory{TDbContext}"/> class.
        /// </summary>
        /// <param name="dbConfigurationOptions">The options.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        protected DatabaseFactory(IOptions<DbConfiguration> dbConfigurationOptions, ILoggerFactory loggerFactory) : base(dbConfigurationOptions, loggerFactory)
        {

        }

        /// <inheritdoc cref="IFactory{TResult}.Create"/>
        public TDbContext Create()
        {
            return base.CreateDbContext(_arguments);
        }
    }
}
