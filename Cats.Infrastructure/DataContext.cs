using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cats.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Cat> cats {get; set;}

        
    }
}
