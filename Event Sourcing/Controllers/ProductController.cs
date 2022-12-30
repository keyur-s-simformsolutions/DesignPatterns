using Event_Sourcing.Data;
using Event_Sourcing.Events;
using Event_Sourcing.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Event_Sourcing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IProductRepository _productRepository;
        public Projection _projection;
        public AppDbContext _dbContext;
        public ProductController(IProductRepository productRepository, AppDbContext dbContext)
        {
            _productRepository = productRepository;
            _dbContext = dbContext;
            _projection = new(_dbContext);
            _productRepository.Subscribe(_projection.ReceiveEvent);
        }
        [HttpGet("{id}")]
        public ActionResult<int> GetQuantity(string id)
        {
            var warehouseProduct = _productRepository.Get(id);

            return warehouseProduct.GetAvailableQuantity();
        }
        [HttpPost("receiveproduct/{id}/{quantity}")]
        public ActionResult<string> ReceiveProduct(string id, int quantity)
        {
            var warehouseProduct = _productRepository.Get(id);

            warehouseProduct.ReceiveProduct(quantity);
            _productRepository.Save(warehouseProduct);
            return Ok("Product received");
        }
        [HttpPost("shipproduct/{id}/{quantity}")]
        public ActionResult<string> ShipProduct(string id, int quantity)
        {
            var warehouseProduct = _productRepository.Get(id);

            warehouseProduct.ShipProduct(quantity);
            _productRepository.Save(warehouseProduct);
            return Ok("Product shiped");
        }
        [HttpGet("events/{id}")]
        public ActionResult<List<string>> GetEvents(string id)
        {
            var warehouseProduct = _productRepository.Get(id);

            var eventlist = warehouseProduct.GetAllEvents();
            List<string> events = new List<string>();
            foreach (IEvent evnt in warehouseProduct.GetAllEvents())
            {
                switch (evnt)
                {
                    case ProductShippedEvent shipProduct:
                        events.Add($"{shipProduct.DateTime} {id} Shipped: {shipProduct.Quantity}");
                        break;
                    case ProductReceivedEvent receiveProduct:
                        events.Add($"{receiveProduct.DateTime} {id} Received: {receiveProduct.Quantity}");
                        break;
                    case InventoryAdjustedEvent inventoryAdjusted:
                        events.Add($"{inventoryAdjusted.DateTime} {id} Adjusted: {inventoryAdjusted.Quantity} {inventoryAdjusted.Reason}");
                        break;
                }

            }
            
            return Ok(events);
        }

    }
}
