using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapBubblesClick
{
    internal class Counter
    {
        public int Value { get; set; } 

        public void Intcrement()
        {
            Value--;
        }

        public void Reset()
        { 
            Value = 30;
        }

    }
}
