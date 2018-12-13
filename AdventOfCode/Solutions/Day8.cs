using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{

    class Day8
    {
        private string path = "../../Input/input8.txt";
        bool parsed = false;
        string line;
        int[] inputNums;
      

        private void ParseInput(string path)
        {
            if (parsed)
                return;

            StreamReader reader = new StreamReader(path);

            while ((line = reader.ReadLine()) != null)
            {
                string[] stringSplit = line.Split(' ').ToArray();
                inputNums = new int[stringSplit.Length];

                for(int i = 0; i < stringSplit.Length; i++)
                {
                    inputNums[i] = int.Parse(stringSplit[i]);
                }
            }

            reader.Close();

            parsed = true;
        }

        public void GetAnswerA()
        {
            Console.WriteLine("Answer A: ");
            ParseInput(path);
           
        }

        public void GetAnswerB()
        {
            Console.WriteLine("Answer B: ");
            ParseInput(path);

        }


    }
}
