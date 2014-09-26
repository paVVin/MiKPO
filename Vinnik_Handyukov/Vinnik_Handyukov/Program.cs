using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace Vinnik_Handyukov
{
    public class Program
    {
        public struct data
        {
            public double a;
            public double b;
            public double alpha;
            public double c;
            public bool norm;
        }
        static int count = 0;
        static string in_file;
        static string out_file;
        public static bool check(string[] raw)
        {
            bool result = false;
            try
            {
                double a1 = Convert.ToDouble(raw[0]);
                double b1 = Convert.ToDouble(raw[1]);
                double alpha1 = Convert.ToDouble(raw[2]);
                if (a1 > 0 && b1 > 0 && alpha1 > 0 && alpha1 < 180) result = true;
                else Console.WriteLine("Ошибка данных в {0} строке.", count);
            }
            catch
            {
                Console.WriteLine("Ошибка данных в {0} строке.", count);
            }
            return result;
        }

        public static double ToRad(double alpha)
        {
            return alpha * Math.PI / 180;
        }

        public static double ToGrad(double alpha)
        {
            return alpha * 180 / Math.PI;
        }

        public static double calcC(data raw)
        {
            double angleRad=ToRad(raw.alpha);
            double C = Math.Sqrt(Math.Pow(raw.a, 2) + Math.Pow(raw.b, 2) - 2 * raw.a * raw.b * Math.Cos(angleRad));
            return C;
        }

        public static double calcBeta(data raw)
        {
            double angleGrad = 0;
            double buf = (Math.Pow(raw.c, 2) + Math.Pow(raw.b, 2) - Math.Pow(raw.a, 2)) / (2 * raw.b * raw.c);
            angleGrad = ToGrad(Math.Acos(buf));
            return angleGrad;
        }

        public static double calcHamma(data raw)
        {
            double angleGrad = 0;
            double buf = (Math.Pow(raw.c, 2) + Math.Pow(raw.a, 2) - Math.Pow(raw.b, 2)) / (2 * raw.a * raw.c);
            angleGrad = ToGrad(Math.Acos(buf));
            return angleGrad;
        }

        public static void toFile(data dat)
        {
            StreamWriter sw = new StreamWriter(out_file, true, Encoding.Default);
            sw.WriteLine("{0} ; {1} ; {2}", dat.a, dat.b, dat.c);
            sw.Close();
        }

        public static void calcAll(data[] raw)
        {
            for (int i = 0; i < raw.Length; i++)
            {
                if (raw[i].norm)
                {
                    raw[i].c = calcC(raw[i]);
                    Console.Write(" A - {0} ", raw[i].a);
                    Console.Write(" B - {0} ", raw[i].b);
                    Console.Write(" C - {0} ", raw[i].c);
                    Console.Write(" Alpha - {0} ", raw[i].alpha);
                    Console.Write(" Beta - {0} ", calcBeta(raw[i]));
                    Console.WriteLine(" Hamma - {0} ", calcHamma(raw[i]));
                    toFile(raw[i]);
                }
            }
        }

        public static void Main(string[] args)
        {
            if (args.Length >= 2)
            {
                in_file = args[0];
                out_file = args[1];
                if (File.Exists(in_file))
                {
                    StreamReader sr = new StreamReader(in_file, Encoding.Default);
                    while (!sr.EndOfStream)
                    {
                        if (File.Exists(in_file))
                        {
                            data[] data_arr = new data[50];
                            for (int i = 0; i < 50; i++)
                            {
                                string[] buf = new string[3];
                                string line = "";
                                if ((line = sr.ReadLine()) != null)
                                {
                                    line = line.Replace(".", ",");
                                    buf = line.Split(';');
                                    count++;
                                    data raw_data = new data();
                                    if (check(buf))
                                    {
                                        raw_data.a = Convert.ToDouble(buf[0]);
                                        raw_data.b = Convert.ToDouble(buf[1]);
                                        raw_data.alpha = Convert.ToDouble(buf[2]);
                                        raw_data.c = 0;
                                        raw_data.norm = true;
                                        data_arr[i] = raw_data;
                                    }
                                    else
                                    {
                                        data_arr[i].norm = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Файл кончился. Считано {0} записей", count);
                                    break;
                                }
                            }
                            calcAll(data_arr);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Нет пути входного или выходного файла");
            }
        }
    }
}
