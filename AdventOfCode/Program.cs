﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {


        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //Day1 day1 = new Day1();
            //day1.GetAnswerA();
            //day1.GetAnswerB();

            //Day2 day2 = new Day2();
            //day2.GetAnswerA();
            //day2.GetAnswerB();

            //Day3 day3 = new Day3();
            //day3.OverlappingInches();

            watch.Stop();

            TimeSpan ts = watch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

            Console.WriteLine("Execution Time: " + elapsedTime);
            Console.WriteLine("Press Key To Exit");
            Console.ReadKey();
        }
    }
}