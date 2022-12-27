using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediator.Concrete;

namespace Mediator.Abstact
{
    public interface IMediator
    {
        void SendMessage(Colleague caller, string msg);
    }
}