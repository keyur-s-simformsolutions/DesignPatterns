using System;

namespace Event_Sourcing.Events
{
    public class ProductReceivedEvent : IEvent
    {
        public ProductReceivedEvent(string id, int quantity, DateTime dateTime)
        {
            Id = id;
            Quantity = quantity;
            DateTime = dateTime;
        }

        public string Id { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }
    }

}