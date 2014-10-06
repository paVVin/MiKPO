using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace MedianFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Нужно минимум 2 аргумента");
                return;
            }
            FilteredImage FI = new FilteredImage(args[0], args[1]);


        }
    }
}
