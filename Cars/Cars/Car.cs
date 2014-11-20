using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cars
{
    class Car
    {
        public int num;
        public double tVhod;
        public double tVihod;
        public Car(int num, double tVhod, double tVihod = 0)
        {
            this.num = num;
            this.tVhod = tVhod;
            this.tVihod = tVihod;
        }
    }
}
