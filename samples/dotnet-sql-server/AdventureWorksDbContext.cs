using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
    public class AdventureWorksDbContext : DbContext
    {
        public AdventureWorksDbContext()
        {
        }

        public AdventureWorksDbContext(DbContextOptions<AdventureWorksDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var product = modelBuilder.Entity<Product>();

            product.HasKey(b => b.Id);
            product.HasData(new[] { new Product { Id = 1, Name = "Jeans" }, new Product { Id = 2, Name = "T-shirts" } });
        }

        public virtual DbSet<Product> Products { get; set; }
    }

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
}