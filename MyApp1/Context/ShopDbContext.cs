using Microsoft.EntityFrameworkCore;
using MyApp1.Models;


namespace MyApp1.Context
{
    public class ShopDbContext:DbContext
    {
        public DbSet<Image> Images { get; set; }

        #region OnConfiguring

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=ImageGalleryDB;Trusted_Connection=true;TrustServerCertificate=true");
            base.OnConfiguring(optionsBuilder);
        }

        #endregion
    }
}
