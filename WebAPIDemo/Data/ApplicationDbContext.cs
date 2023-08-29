using Microsoft.EntityFrameworkCore;
using WebAPIDemo.Models.CustomValidations;

namespace WebAPIDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Shirt> Shirts { get; set; }

        public ApplicationDbContext(DbContextOptions options):base(options) 
        {
        }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Shirt>().HasData(
                new Shirt { Id = 1, Brand = "PUMA", Color = "Blue", Gender = "Men", Size = 10, Price = 2000 },
                new Shirt { Id = 2, Brand = "NIKE", Color = "Green", Gender = "Women", Size = 7, Price = 3000 },
                new Shirt { Id = 3, Brand = "PUMA", Color = "Yellow", Gender = "Men", Size = 9, Price = 4000 },
                new Shirt { Id = 4, Brand = "NIKE", Color = "Red", Gender = "Women", Size = 8, Price = 5000 }
            );
        }
    }
}
