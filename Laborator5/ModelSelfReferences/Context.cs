using ModelSelfReferences.Case4;
using ModelSelfReferences.Case5;

namespace ModelSelfReferences
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Context : DbContext
    {
        // Your context has been configured to use a 'Context' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ModelSelfReferences.Context' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Context' 
        // connection string in the application configuration file.
        public Context()
            : base(@"Context")
        {
        }

        public virtual DbSet<SelfReference> SelfReferences { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Photograph> Photographs { get; set; }
        public virtual DbSet<PhotographFullImage> PhotographFullImages { get; set; }

        public virtual DbSet<Business> Businesses { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // case1
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SelfReference>()
                .HasMany<SelfReference>(e => e.References)
                .WithOptional(m => m.ParentSelfReference);

            // case2
            modelBuilder.Entity<Product>()
                .Map(m =>
                {
                    m.Properties(p => new {p.SKU, p.Description, p.Price});
                    m.ToTable("Product", "BazaDeDate");
                })
                .Map(m =>
                {
                    m.Properties(p => new {p.SKU, p.ImageURL});
                    m.ToTable("ProductWebInfo", "BazaDeDate");
                });

            // case3
            modelBuilder.Entity<Photograph>()
                .HasRequired(p => p.PhotographFullImage)
                .WithRequiredPrincipal(p => p.Photograph);
            modelBuilder.Entity<Photograph>()
                .ToTable("Photograph", "BazaDeDate");
            modelBuilder.Entity<PhotographFullImage>()
                .ToTable("Photograph", "BazaDeDate");

            

            // case5
            modelBuilder.Entity<Employee>()
                .Map<FullTimeEmployee>(m => m.Requires("EmployeeType")
                    .HasValue(1))
                .Map<HourlyEmployee>(m => m.Requires("EmployeeType")
                    .HasValue(2));
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}