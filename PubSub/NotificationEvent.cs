using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubSub
{
    internal class NotificationEvent
    {
        public string NotificationMessage { get; private set; }

        public DateTime NotificationDate { get; private set; }

        public NotificationEvent(DateTime _dateTime, string _message)
        {
            NotificationDate = _dateTime;
            NotificationMessage = _message;
        }
    }
}
