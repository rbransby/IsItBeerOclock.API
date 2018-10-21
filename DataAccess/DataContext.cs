using Lib.Net.Http.WebPush;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsItBeerOclock.API.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {            
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<PushSubscription> PushSubscriptions { get; set; }

        public DbSet<PushSubscriptionKey> PushSubscriptionKeys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=IsItBeerOclock.db");
        }
    }
}
