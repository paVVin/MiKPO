using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FuncTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Process proc = new Process();
            string path="C:/Users/PAVVIN/Desktop/Downloaded/MiKPO-master/MiKPO-master/Vinnik_Handyukov/Vinnik_Handyukov/bin/Debug/";
            proc.StartInfo.FileName = path+"Vinnik_Handyukov.exe";
            proc.StartInfo.Arguments = path + "in.txt " + path + "outFunc.txt";
            proc.Start();
            proc.Close();

            proc.StartInfo.Arguments = path + "in123123.txt " + path + "outFunc1.txt";
            proc.Start();
            proc.Close();

            proc.StartInfo.Arguments = path + "outFunc2.txt";
            proc.Start();
            proc.Close();
        }
    }
}
