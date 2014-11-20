using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cars
{
    class Program
    {
        static Queue<Car>[] Cars = null;
        static Random rnd = new Random();
        static System.Threading.Thread[] threads;

        public static void Service(Queue<Car>[] Cars,int cashnum,int tObr)
        {
            while (true)
            {
                while (Cars[cashnum].Count == 0)
                    System.Threading.Thread.Sleep(0);

                Car car = Cars[cashnum].Dequeue();
                car.tVihod = car.tVhod + tObr;

                Console.WriteLine("Машина {0}. Вход - {1}, Выход - {2}", car.num.ToString(), car.tVhod, car.tVihod);

                System.Threading.Thread.Sleep(tObr * 1000);
            }
        }

        public static void AddCars(Queue<Car>[] Cars, int countCar, int countCashes)
        {
            Random rnd = new Random();
            int carNumber = 0;
            double tNow = 0;
            int sleep = 1000;

            while (true)
            {
                for (int i = 0; i < countCar; i++)
                {
                    int cashnum = rnd.Next(countCashes);
                    Car car = new Car(carNumber++, tNow / 1000);
                    Cars[cashnum].Enqueue(car);
                    tNow += sleep;
                    Console.WriteLine("Машина {0} в кассу {1}", car.num.ToString(), cashnum);
                }
                System.Threading.Thread.Sleep(sleep);
            }
        }

        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Необходимо 4 аргумента");
                return;
            }
            int countCashes = int.Parse(args[0]);
            int countIn = int.Parse(args[1]);
            int tObr = int.Parse(args[2]);
            int tTotal = int.Parse(args[3]);

            Cars = new Queue<Car>[countCashes];
            threads = new System.Threading.Thread[countCashes];

            for (int i = 0; i < countCashes; i++)
            {
                Cars[i] = new Queue<Car>();
                int i1 = i;
                threads[i] = new System.Threading.Thread(() => Service(Cars, i1, tObr));
                threads[i].IsBackground = false;
            }

            for (int k = 0; k < countCashes; k++)
                threads[k].Start();

            System.Threading.Thread manager = new System.Threading.Thread(() => AddCars(Cars, countIn, countCashes));
            manager.IsBackground = false;
            manager.Start();

            System.Threading.Thread.Sleep(tTotal * 1000);

            for (int i = 0; i < countCashes; i++)
                threads[i].Abort();
            manager.Abort();

            int notServiced = Cars[0].Count;
            for (int i = 1; i < countCashes; i++)
                notServiced += Cars[i].Count;

            Console.WriteLine("Не обслужено машин: {0}", notServiced);
            Console.ReadKey();



        }
    }
}
