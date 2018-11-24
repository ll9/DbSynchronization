using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DbSyncAlgorithm.Models
{
    public class DbSyncAlgorithmContext : DbContext
    {
        public DbSyncAlgorithmContext (DbContextOptions<DbSyncAlgorithmContext> options)
            : base(options)
        {
        }

        public DbSet<DbSyncAlgorithm.Models.Person> Person { get; set; }
    }
}
