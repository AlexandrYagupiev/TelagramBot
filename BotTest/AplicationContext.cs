using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotTest
{
    public class AplicationContext : DbContext
    {
        public DbSet<Application> Applications { get; set; }

        public AplicationContext(string strConnection):base(new DbContextOptionsBuilder().UseSqlServer(strConnection).Options)
        {
            this.Database.EnsureCreated();
            
        }
    }
}
