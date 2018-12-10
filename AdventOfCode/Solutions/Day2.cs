using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    public class Day2
    {
        StreamReader readFile = new StreamReader("../../Input/input2.txt");

        public void GetAnswerA()
        {
            int doubleCount = 0, tripleCount = 0;
            string line;

            while ((line = readFile.ReadLine()) != null)
            {
                bool dblFlag = false, tplFlag = false;
                var groupsOfChars = line.GroupBy(oChar => oChar);
                groupsOfChars.ToList().ForEach(x =>
                {
                    if (x.Count() == 2) dblFlag = true;
                    if (x.Count() == 3) tplFlag = true;
                    return;
                }
                );

                if (dblFlag) doubleCount++;
                if (tplFlag) tripleCount++;
            }

            Console.WriteLine(doubleCount * tripleCount);
        }

        public void GetAnswerB()
        {
            string line;
            List<string> lines = new List<string>();

            while ((line = readFile.ReadLine()) != null)
            {
                lines.Add(line);
            }

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i + 1; j < lines.Count; j++)
                {
                    int distance = 0;
                    char[] lineI = lines[i].ToCharArray(), lineJ = lines[j].ToCharArray();

                    distance = lineI.Zip(lineJ, (a, b) => new { a, b })
                        .Count(m => m.a != m.b);

                    //Console.WriteLine(string.Format("{0} | {1} : {2}", i, j, distance));

                    if (distance == 1)
                    {
                        string removedLetter = "";
                        for(int x = 0; x < lineI.Length; x++)
                        {
                            
                            if(lineI[x] != lineJ[x])
                            {
                                removedLetter = lines[i].Remove(x, 1);
                            }
                        }

                        Console.WriteLine(lines[i] + " and \n" + lines[j]);
                        Console.WriteLine(removedLetter);
                        return;
                    }
                }
            }

        }
    }
}
