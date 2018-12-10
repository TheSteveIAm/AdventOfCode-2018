using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    public class Day1
    {
        StreamReader readFile = new StreamReader("../../Input/input.txt");
        string line;
        List<int> lines = new List<int>();
        bool parsed = false;

        public void ParseInput()
        {
            if (parsed)
            {
                return;
            }

            while ((line = readFile.ReadLine()) != null)
            {
                lines.Add(int.Parse(line));
            }
            parsed = true;
        }

        public void GetAnswerA()
        {
            ParseInput();
            var frequencies = 0;
            foreach (var value in lines)
            {
                frequencies = frequencies + value;
            }
            Console.WriteLine("Final frequency: " + frequencies.ToString());
        }

        public void GetAnswerB()
        {
            ParseInput();

            var frequency = 0;
            var frequenciesList = new HashSet<int>();
            var flag = true;

            while (flag)
            {
                foreach (var value in lines)
                {
                    frequency += value;
                    if (frequenciesList.Contains(frequency))
                    {
                        flag = false;
                        break;
                    }
                    frequenciesList.Add(frequency);
                }
            }
            Console.WriteLine("First Repeated Frequency: " +  frequency.ToString());
        }
    }
}