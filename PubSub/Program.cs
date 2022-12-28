using System;
using System.Threading.Tasks;

namespace PubSub
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating Instance of Publisher
            PubSub.Publisher.Publisher stackoverflow = new PubSub.Publisher.Publisher("StackOverflow.Com", 3000);
            PubSub.Publisher.Publisher facebook = new PubSub.Publisher.Publisher("Facebook.com", 1000);

            //Create Instances of Subscribers
            PubSub.Subscriber.Subscriber sub1 = new PubSub.Subscriber.Subscriber("Keyur");
            PubSub.Subscriber.Subscriber sub2 = new PubSub.Subscriber.Subscriber("Mahesh");
            PubSub.Subscriber.Subscriber sub3 = new PubSub.Subscriber.Subscriber("Manan");

            //Pass the publisher obj to their Subscribe function
            sub1.Subscribe(facebook);
            sub3.Subscribe(facebook);

            sub1.Subscribe(stackoverflow);
            sub2.Subscribe(stackoverflow);

            //sub1.Unsubscribe(facebook);


            //Concurrently running multiple publishers thread
            Task task1 = Task.Factory.StartNew(() => facebook.Publish());
            Task task2 = Task.Factory.StartNew(() => stackoverflow.Publish());
            Task.WaitAll(task1, task2);

        }
    }
}