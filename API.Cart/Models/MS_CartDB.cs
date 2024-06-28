using Microsoft.EntityFrameworkCore;
    
namespace API.Cart.Models
{
    public class MS_CartDB : DbContext
    {
        public MS_CartDB(DbContextOptions<MS_CartDB> options) : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }
    }
}