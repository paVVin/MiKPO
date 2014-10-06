using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace MedianFilter
{
    public class FilteredImage
    {
        public string path_in_file;
        public string path_out_file;
        public Bitmap original;
        public Bitmap filtered;
        public static int net = 3;
        Byte[,] Red;
        Byte[,] Green;
        Byte[,] Blue;

        private void BitmapToArrays(Bitmap file, ref byte[,] red, ref byte[,] green, ref byte[,] blue)
        {
            for (int y = 0; y < file.Height; y++)
                for (int x = 0; x < file.Width; x++)
                {
                    Color c = file.GetPixel(x, y);
                    red[x, y] = (Byte)c.R;
                    green[x, y] = (Byte)c.G;
                    blue[x, y] = (Byte)c.B;
                    filtered.SetPixel(x, y, Color.Black);
                }
        }

        private Bitmap ArraysToBitmap(Byte[,] red, Byte[,] green, Byte[,] blue)
        {
            Bitmap result = filtered;
            for (int x = 0; x < result.Width; x++)
                for (int y = 0; y < result.Height; y++)
                {
                    Color c = Color.FromArgb(red[x, y], green[x, y], blue[x, y]);
                    result.SetPixel(x, y, c);
                }
            return result;
        }

        public void Median(Object arr1)
        {
            Byte[,] arr = (Byte[,])arr1;
            Byte[] buf = new Byte[net * net];

            int Nh = 1;
            int heig = arr.GetLength(1);
            int wid = arr.GetLength(0);
            int i, y, x, yy, yyy, xx, xxx;
            for (y = Nh; y < heig - Nh; y++) 
            {
                for (x = Nh; x < wid - Nh; x++) 
                {
                    i = 0;
                    for (yy = -Nh; yy <= Nh; yy++) 
                    {
                        yyy = y + yy;
                        for (xx = -Nh; xx <= Nh; xx++)
                        {
                            xxx = x + xx;
                            buf[i] = arr[yyy, xxx];
                            i++;
                        } 
                    }  
                    Array.Sort(buf);
                    arr[y, x] = buf[4];
                } 
            } 
        }

        static void Wait(List<Thread> ThreadList)
        {
            while (true)
            {
                int WorkCount = 0;

                for (int i = 0; i < ThreadList.Count; i++)
                {
                    WorkCount += (ThreadList[i].IsAlive) ? 0 : 1;
                }

                if (WorkCount == ThreadList.Count)
                    break;
            }
        }

        public void Filter(Bitmap file)
        {
            Red = new Byte[file.Height, file.Width];
            Green = new Byte[file.Height, file.Width];
            Blue = new Byte[file.Height, file.Width];

            BitmapToArrays(file, ref Red, ref Green, ref Blue);

            Object obRed = (Object)Red;
            Object obGreen = (Object)Green;
            Object obBlue = (Object)Blue;

            Thread thr1 = new Thread(Median);
            Thread thr2 = new Thread(Median);
            Thread thr3 = new Thread(Median);

            List<Thread> ThreadList = new List<Thread>();
            ThreadList.Add(thr1);
            ThreadList.Add(thr2);
            ThreadList.Add(thr3);

            thr1.Start(obRed);
            thr2.Start(obGreen);
            thr3.Start(obBlue);

            Wait(ThreadList);

            filtered = ArraysToBitmap(Red, Green, Blue);
            filtered.Save(path_out_file, System.Drawing.Imaging.ImageFormat.Bmp);
            Console.WriteLine("Файл успешно отфильтрован");
        }

        public FilteredImage() { }

        public FilteredImage(string in_f, string out_f)
        {
            try
            {
                this.original = new Bitmap(Image.FromFile(in_f));
                this.filtered = new Bitmap(Image.FromFile(in_f));
                path_out_file = out_f;
                Filter(filtered);
            }
            catch
            {
                Console.WriteLine("Отстутствует файл-источник или файл слишком мал");
            }
        }
    }
}
