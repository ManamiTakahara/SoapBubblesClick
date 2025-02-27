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
        public int Value2 { get; set; }

        public void Intcrement()
        {
            Value += 50;
        }

        public void CountDoun()
        {
            Value2 -= 1;
        }

        public void Reset()
        { 
            Value = 0;
        }

    }
}
