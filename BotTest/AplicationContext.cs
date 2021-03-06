using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotTest
{
    public class AplicationContext : DbContext
    {
        public DbSet<ApplicationModel> Applications { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<PhotoPathModel> Photos { get; set; }

        public AplicationContext(string strConnection):base(new DbContextOptionsBuilder().UseSqlServer(strConnection).Options)
        {
            this.Database.EnsureCreated();           
        }
    }
}
