using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vinnik_Handyukov;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestCheck1()
        {
            string[] raw = new string[3] { "1", "2", "60" };
            bool expect = true;
            bool actual = Program.check(raw);
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void TestCheck2()
        {
            string[] raw = new string[3] { "4,4", "2,0", "120" };
            bool expect = true;
            bool actual = Program.check(raw);
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void TestCheck3()
        {
            string[] raw = new string[3] { "3", "4", "10" };
            bool expect = true;
            bool actual = Program.check(raw);
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void TestToRad()
        {
            double expect = 0.9599;
            double actual = Program.ToRad(55);
            Assert.AreEqual(expect, actual, 0.0001);
        }

        [TestMethod]
        public void TestToGRad()
        {
            double expect = 180;
            double actual = Math.Round(Program.ToGrad(3.1416), 4);
            Assert.AreEqual(expect, actual, 0.001);
        }

        [TestMethod]
        public void TestCalcC()
        {
            Program.data raw = new Program.data() { a = 5, b = 5, alpha = 60 };
            double expect = 5;
            double actual = Program.calcC(raw);
            Assert.AreEqual(expect, actual, 0.1);
        }

        [TestMethod]
        public void TestCalcBeta()
        {
            Program.data raw = new Program.data() { a = 5, b = 5, alpha = 60, c = 5 };
            double expect = 60;
            double actual = Program.calcBeta(raw);
            Assert.AreEqual(expect, actual, 0.1);
        }

        [TestMethod]
        public void TestCalcHamma()
        {
            Program.data raw = new Program.data() { a = 5, b = 5, alpha = 60, c = 5 };
            double expect = 60;
            double actual = Program.calcHamma(raw);
            Assert.AreEqual(expect, actual, 0.1);
        }

        [TestMethod]
        public void TestMain1()
        {

            string[] arg = new string[2] { 
                "C:/Users/PAVVIN/Desktop/Downloaded/MiKPO-master/MiKPO-master/Vinnik_Handyukov/Vinnik_Handyukov/bin/Debug/in.txt", 
                "C:/Users/PAVVIN/Desktop/Downloaded/MiKPO-master/MiKPO-master/Vinnik_Handyukov/Vinnik_Handyukov/bin/Debug/out.txt" };
            Program.Main(arg);
        }

        [TestMethod]
        public void TestMain2()
        {

            string[] arg = new string[2] { 
                "C:/Users/PAVVIN/Desktop/Downloaded/MiKPO-master/MiKPO-master/Vinnik_Handyukov/Vinnik_Handyukov/bin/Debug/in_NETEGO.txt", 
                "C:/Users/PAVVIN/Desktop/Downloaded/MiKPO-master/MiKPO-master/Vinnik_Handyukov/Vinnik_Handyukov/bin/Debug/out1.txt" };
            Program.Main(arg);
        }

        [TestMethod]
        public void TestMain3()
        {

            string[] arg = new string[2] { 
                "C:/Users/PAVVIN/Desktop/Downloaded/MiKPO-master/MiKPO-master/Vinnik_Handyukov/Vinnik_Handyukov/bin/Debug/in1.txt", 
                "C:/Users/PAVVIN/Desktop/Downloaded/MiKPO-master/MiKPO-master/Vinnik_Handyukov/Vinnik_Handyukov/bin/Debug/out2.txt" };
            Program.Main(arg);
        }

        [TestMethod]
        public void TestMain4()
        {

            string[] arg = new string[1] { 
                "C:/Users/PAVVIN/Desktop/Downloaded/MiKPO-master/MiKPO-master/Vinnik_Handyukov/Vinnik_Handyukov/bin/Debug/in.txt" };
            Program.Main(arg);
        }
    }
}
