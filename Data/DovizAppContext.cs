using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DovizApp.Models
{
    public class DovizAppContext : DbContext
    {
        public DovizAppContext (DbContextOptions<DovizAppContext> options)
            : base(options)
        {
        }

        public DbSet<DovizApp.Models.Doviz> Doviz { get; set; }
    }
}
