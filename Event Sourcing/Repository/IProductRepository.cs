using Event_Sourcing.Data;
using Event_Sourcing.Events;

namespace Event_Sourcing.Repository
{
    public interface IProductRepository
    {
        public WarehouseProduct Get(string id);
        public void Save(WarehouseProduct warehouseProduct);
        public void Subscribe(Action<IEvent> callback);
    }
}
