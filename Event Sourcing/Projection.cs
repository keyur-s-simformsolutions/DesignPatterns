using Event_Sourcing.Data;
using Event_Sourcing.Events;
using System.Linq;

namespace Event_Sourcing
{
    public class Projection
    {
        private readonly AppDbContext _context;

        public Projection(AppDbContext context)
        {
            _context = context;
        }

        public void ReceiveEvent(IEvent evnt)
        {
            switch (evnt)
            {
                case ProductShippedEvent shipProduct:
                    Apply(shipProduct);
                    break;
                case ProductReceivedEvent receiveProduct:
                    Apply(receiveProduct);
                    break;
            }
        }

        public Product GetProduct(string id)
        {
            Product product = _context.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                product = new Product
                {
                    Id = id
                };
                _ = _context.Products.Add(product);
            }

            return product;
        }

        private void Apply(ProductShippedEvent shipProduct)
        {
            Product product = GetProduct(shipProduct.Id);
            product.Shipped += shipProduct.Quantity;
            _ = _context.SaveChanges();
        }

        private void Apply(ProductReceivedEvent productReceived)
        {
            Product state = GetProduct(productReceived.Id);
            state.Received += productReceived.Quantity;
            _ = _context.SaveChanges();
        }
    }
}