using MassTransit;
using EventBus;
using API.Cart.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Cart.Consumers
{
    public class ProductUpdatedConsumer: IConsumer<ProductUpdated>
    {
        private readonly MS_CartDB _context;

        public ProductUpdatedConsumer(MS_CartDB context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<ProductUpdated> prodContext)
        {
            Console.WriteLine("Consuming product updated " + prodContext.Message.Id);

            var carts = await _context.Carts.FirstAsync(c => c.ProductId == prodContext.Message.Id);
            carts.Price = prodContext.Message.Price;
            var result = await _context.SaveChangesAsync();

            if (result == 0)
                throw new MessageException(typeof(ProductUpdated), "Problem updating product");
        }
    }
}
