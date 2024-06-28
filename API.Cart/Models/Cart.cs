using System.ComponentModel.DataAnnotations;

namespace API.Cart.Models
{
    public class Cart
    {
        [Key]
        public int ProductId { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
    }
}
