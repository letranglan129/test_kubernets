using MassTransit;
using EventBus;
using API.Cart.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Cart.Consumers
{
    public class ProductDeletedConsumer : IConsumer<ProductDeleted>
    {
        private readonly MS_CartDB _context;

        public ProductDeletedConsumer(MS_CartDB context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<ProductDeleted> prodContext)
        {
            prodContext.CancellationToken.ThrowIfCancellationRequested();
            Console.WriteLine("Consuming product delete " + prodContext.Message.Id);

            var carts = await _context.Carts.Where(c => c.ProductId == prodContext.Message.Id).ToListAsync();
            _context.RemoveRange(carts);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
                throw new MessageException(typeof(ProductDeleted), "Problem deleting product");
        }
    }
}
