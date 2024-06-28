using Microsoft.EntityFrameworkCore;
    
namespace API.Product.Models
{
    public class MS_ProductDB : DbContext
    {
        public MS_ProductDB(DbContextOptions<MS_ProductDB> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}