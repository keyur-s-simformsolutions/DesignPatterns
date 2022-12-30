using Event_Sourcing.Data;
using Event_Sourcing.Events;
using System;
using System.Collections.Generic;

namespace Event_Sourcing.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Action<IEvent>> _projectionCallbacks = new();
        private readonly Dictionary<string, List<IEvent>> _inMemoryStreams = new();

        public WarehouseProduct Get(string id)
        {
            WarehouseProduct warehouseProduct = new(id);

            if (_inMemoryStreams.ContainsKey(id))
            {
                foreach (IEvent evnt in _inMemoryStreams[id])
                {
                    warehouseProduct.ApplyEvent(evnt);
                }
            }

            return warehouseProduct;
        }

        public void Save(WarehouseProduct warehouseProduct)
        {
            if (_inMemoryStreams.ContainsKey(warehouseProduct.Id) == false)
            {
                _inMemoryStreams.Add(warehouseProduct.Id, new List<IEvent>());
            }

            List<IEvent> newEvents = warehouseProduct.GetUncommittedEvents();
            _inMemoryStreams[warehouseProduct.Id].AddRange(newEvents);
            warehouseProduct.EventsCommitted();

            foreach (IEvent newEvent in newEvents)
            {
                foreach (Action<IEvent> callback in _projectionCallbacks)
                {
                    callback(newEvent);
                }
            }
        }

        public void Subscribe(Action<IEvent> callback)
        {
            _projectionCallbacks.Add(callback);
        }
    }
}