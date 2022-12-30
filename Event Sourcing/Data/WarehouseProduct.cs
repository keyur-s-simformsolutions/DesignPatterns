using Event_Sourcing.Events;
using System;
using System.Collections.Generic;

namespace Event_Sourcing.Data
{
    public class CurrentState
    {
        public int AvailableQuantity { get; set; }
    }

    public class WarehouseProduct
    {
        public string Id { get; }
        private readonly List<IEvent> _allEvents = new();
        private readonly List<IEvent> _uncommittedEvents = new();

        // Projection
        private readonly CurrentState _currentState = new();

        public WarehouseProduct(string id)
        {
            Id = id;
        }

        public void ShipProduct(int quantity)
        {
            if (quantity > _currentState.AvailableQuantity)
            {
                throw new Exception("Insufficient quantity");
            }

            AddEvent(new ProductShippedEvent(Id, quantity, DateTime.Now));
        }

        public void ReceiveProduct(int quantity)
        {
            AddEvent(new ProductReceivedEvent(Id, quantity, DateTime.Now));
        }

        public void AdjustInventory(int quantity, string reason)
        {
            if (_currentState.AvailableQuantity + quantity < 0)
            {
                throw new Exception("Cannot adjust to a negative available quantity");
            }

            AddEvent(new InventoryAdjustedEvent(Id, quantity, reason, DateTime.Now));
        }

        private void Apply(ProductShippedEvent evnt)
        {
            _currentState.AvailableQuantity -= evnt.Quantity;
        }

        private void Apply(ProductReceivedEvent evnt)
        {
            _currentState.AvailableQuantity += evnt.Quantity;
        }

        private void Apply(InventoryAdjustedEvent evnt)
        {
            _currentState.AvailableQuantity += evnt.Quantity;
        }

        public void ApplyEvent(IEvent evnt)
        {
            switch (evnt)
            {
                case ProductShippedEvent shipProduct:
                    Apply(shipProduct);
                    break;
                case ProductReceivedEvent receiveProduct:
                    Apply(receiveProduct);
                    break;
                case InventoryAdjustedEvent inventoryAdjusted:
                    Apply(inventoryAdjusted);
                    break;
                default:
                    throw new InvalidOperationException("Unsupported Event.");
            }

            _allEvents.Add(evnt);
        }

        public void AddEvent(IEvent evnt)
        {
            ApplyEvent(evnt);
            _uncommittedEvents.Add(evnt);
        }

        public List<IEvent> GetUncommittedEvents()
        {
            return new List<IEvent>(_uncommittedEvents);
        }

        public List<IEvent> GetAllEvents()
        {
            return new List<IEvent>(_allEvents);
        }

        public void EventsCommitted()
        {
            _uncommittedEvents.Clear();
        }

        public int GetAvailableQuantity()
        {
            return _currentState.AvailableQuantity;
        }
    }
}