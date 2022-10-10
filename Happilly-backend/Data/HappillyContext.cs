using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Happilly_backend.Models;

namespace Happilly_backend.Data
{
    public class HappillyContext : DbContext
    {
        public HappillyContext (DbContextOptions<HappillyContext> options)
            : base(options)
        {
        }

        public DbSet<Happilly_backend.Models.Reminder> Reminder { get; set; } = default!;

        public DbSet<Happilly_backend.Models.Medicine> Medicine { get; set; }
    }
}
