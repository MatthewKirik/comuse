using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ComuseContext : DbContext
    {
        public ComuseContext(DbContextOptions<ComuseContext> options)
            : base(options)
        {
            Database.Migrate();
        }
        public DbSet<UserEntity> Users { get; set; }
    }
}

