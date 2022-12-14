using Happilly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happilly.Persistence.Configurations
{
    /// <summary>
    /// Represents the <see cref="ReminderConfiguration"/> class used to configure the relations and columns in the <see cref="DbSet{TEntity}"/> for <see cref="Reminder"/> in the DbContext.
    /// </summary>
    public class ReminderConfiguration : IEntityTypeConfiguration<Reminder>
    {
        /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}.Configure"/>
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasIndex(p => p.Name).IsUnique();
            builder.HasMany(p => p.Medicines).WithMany(p => p.Reminders);
        }
    }
}