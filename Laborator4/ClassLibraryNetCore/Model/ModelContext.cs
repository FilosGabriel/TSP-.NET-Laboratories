using Microsoft.EntityFrameworkCore;

namespace Lab4.Model
{
    public class ModelContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=DESKTOP-7RQCL7U\FILO;Initial Catalog=LabEfCore;Integrated Security=True");
        }

    }
}