using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Asp_Filos_Gabriel_Rp.Models;

namespace Asp_Filos_Gabriel_Rp.Data
{
    public class Asp_Filos_Gabriel_RpContext : DbContext
    {
        public Asp_Filos_Gabriel_RpContext (DbContextOptions<Asp_Filos_Gabriel_RpContext> options)
            : base(options)
        {
        }

        public DbSet<Asp_Filos_Gabriel_Rp.Models.Movie> Movie { get; set; }
    }
}
