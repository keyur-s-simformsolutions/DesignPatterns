using Abstract.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstract
{
    internal interface IAbstractFactory
    {
       IAbstractProductA CreateProductA();

       IAbstractProductB CreateProductB();
    }
}
