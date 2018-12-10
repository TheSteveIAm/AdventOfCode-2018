using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    class Day3
    {
        private string path = "../../Input/input3.txt";
        private HashSet<int>[,] clothGrid;
        private List<string> lines = new List<string>();
        private int squaresWithTwoClains = 0;

        public void ParseInput(string path)
        {
            StreamReader reader = new StreamReader(path);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line.Trim());
            }

            reader.Close();
        }


        public void OverlappingInches()
        {

            ParseInput(path);

            clothGrid = new HashSet<int>[1000, 1000];

            foreach (string line in lines)
            {
                string[] splits = line.Split('@');
                string[] splitId = splits[0].Trim().Split('#');
                string[] splitColon = splits[1].Split(':');
                string[] splitCommas = splitColon[0].Trim().Split(',');
                string[] splitX = splitColon[1].Split('x');

                int id = int.Parse(splitId[1]);
                int x = int.Parse(splitCommas[0]);
                int y = int.Parse(splitCommas[1]);
                int xLength = int.Parse(splitX[0]);
                int yLength = int.Parse(splitX[1]);

                //calc inches with 2+ claims
                for (int xPos = x; xPos < (x + xLength); xPos++)
                {
                    for (int yPos = y; yPos < (y + yLength); yPos++)
                    {
                        if (clothGrid[xPos, yPos] == null)
                            clothGrid[xPos, yPos] = new HashSet<int>();

                        clothGrid[xPos, yPos].Add(id);
                        if (clothGrid[xPos, yPos].Count == 2)
                        {
                            squaresWithTwoClains++;
                        }
                    }
                }
            }

            Console.WriteLine("Inches with 2+ claims: " + squaresWithTwoClains);

            for (int i = 1; i < lines.Count + 1; i++)
            {
                //find isolated claim
                bool notIsolated = false;

                for (int xPos = 0; xPos < 1000; xPos++)
                {
                    for (int yPos = 0; yPos < 1000; yPos++)
                    {

                        if (clothGrid[xPos, yPos] != null && clothGrid[xPos, yPos].Contains(i) && clothGrid[xPos, yPos].Count > 1)
                        {
                            notIsolated = true;
                            continue;
                        }
                    }

                    if (notIsolated)
                        continue;
                }

                if (notIsolated)
                    continue;

                Console.WriteLine("Claim with no overlaps: " + i);

            }
        }

    }
}