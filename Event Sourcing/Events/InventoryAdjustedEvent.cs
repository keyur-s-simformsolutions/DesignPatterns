using System;

namespace Event_Sourcing.Events
{
    public class InventoryAdjustedEvent : IEvent
    {
        public InventoryAdjustedEvent(string id, int quantity, string reason, DateTime dateTime)
        {
            Id = id;
            Quantity = quantity;
            Reason = reason;
            DateTime = dateTime;
        }

        public string Id { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }
        public string Reason { get; set; }
    }

}