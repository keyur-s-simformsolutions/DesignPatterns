using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubSub.Subscriber
{
    class Subscriber{
    public string SubscriberName { get; private set; }

    public Subscriber(string _subscriberName){
        SubscriberName = _subscriberName;
    }
    
    // This function subscribe to the event if it is raised by the Publisher
    public void Subscribe(PubSub.Publisher.Publisher p){

        // register OnNotificationReceived with publisher event
        p.OnPublish += OnNotificationReceived;  // multicast delegate 
    }

    // This function unsubscribe from the event if it is raised by the Publisher
    public void Unsubscribe(PubSub.Publisher.Publisher p){

        // unregister OnNotificationReceived from publisher
        p.OnPublish -= OnNotificationReceived;  // multicast delegate 
    }

    // It get executed when the event published by the Publisher
    protected virtual void OnNotificationReceived(PubSub.Publisher.Publisher p, NotificationEvent e){
    
        Console.WriteLine("Hey " + SubscriberName + ", " + e.NotificationMessage +" - "+ p.PublisherName + " at " + e.NotificationDate);
    }
}
}
