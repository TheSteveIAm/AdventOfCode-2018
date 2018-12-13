using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{

    public class Coordinate
    {
        public int x;
        public int y;
    }

    class Day6
    {

        private string path = "../../Input/input6.txt";
        bool parsed = false;
        string line;
        Dictionary<int, Coordinate> coordinates = new Dictionary<int, Coordinate>();
        int[] areaCountForIds;
        HashSet<int> idsWithInfiniteRange = new HashSet<int>();
        int areaOfB;

        int[,] grid;

        private void ParseInput(string path)
        {
            if (parsed)
                return;

            StreamReader reader = new StreamReader(path);
            int id = 0;

            while ((line = reader.ReadLine()) != null)
            {
                string[] stringSplit = line.Split(',');
                Coordinate coord = new Coordinate()
                {
                    x = int.Parse(stringSplit[0]),
                    y = int.Parse(stringSplit[1])
                };
                coordinates.Add(id, coord);

                id++;
            }

            areaCountForIds = new int[coordinates.Count];

            reader.Close();

            parsed = true;
        }

        private void BuildGrid()
        {
            //get the highest x and y coordinates
            int topX = coordinates.GroupBy(c => c.Value.x).OrderByDescending(x => x.Key).Select(x => x.Key).First();
            int topY = coordinates.GroupBy(c => c.Value.y).OrderByDescending(y => y.Key).Select(y => y.Key).First();

            grid = new int[topX + 1, topY + 1];

            Console.WriteLine(grid.GetUpperBound(0) + "x" + grid.GetUpperBound(1));

        }

        public void GetAnswer()
        {
            ParseInput(path);
            BuildGrid();

            Console.WriteLine(string.Format("{0}x{1}", grid.GetUpperBound(0), grid.GetUpperBound(1)));
            //loop through whole grid
            for (int x = 0; x < grid.GetUpperBound(0); x++)
            {
                for (int y = 0; y < grid.GetUpperBound(1); y++)
                {
                    int idOfClosestCoordinate = -1;
                    int closestDistance = int.MaxValue;
                    bool matchingDistance = false;
                    int distanceSum = 0;

                    //check each grid point against the input coordinates
                    foreach (var c in coordinates)
                    {
                        int distance = GetManhattanDistance(x, c.Value.x, y, c.Value.y);

                        if (distance < closestDistance)
                        {
                            idOfClosestCoordinate = c.Key;
                            //Console.WriteLine(c.Key);
                            closestDistance = distance;
                            matchingDistance = false;
                        }
                        else if (distance == closestDistance)
                        {
                            matchingDistance = true;
                        }

                        distanceSum += distance;
                    }

                    if (distanceSum < 10000)
                    {
                        areaOfB++;
                    }

                    //check if this coordinate is touching an edge, if so, mark the ID as having an infinite range
                    if (x <= 0 || y <= 0 || x >= grid.GetUpperBound(0) || y >= grid.GetUpperBound(1))
                    {
                        if (!idsWithInfiniteRange.Contains(idOfClosestCoordinate))
                            idsWithInfiniteRange.Add(idOfClosestCoordinate);
                    }

                    //mark grid location with closest id or -1 for locations that are equidistant from other coordinates

                    grid[x, y] = matchingDistance ? -1 : idOfClosestCoordinate;

                }
            }
            Console.WriteLine("B Area: " + areaOfB);

            for (int x = 0; x < grid.GetUpperBound(0); x++)
            {
                for (int y = 0; y < grid.GetUpperBound(1); y++)
                {
                    if (grid[x, y] > -1)
                        areaCountForIds[grid[x, y]]++;
                }
            }

            foreach (int id in idsWithInfiniteRange)
            {
                areaCountForIds[id] = -1;
            }

            int largestArea = areaCountForIds.OrderByDescending(x => x).ToList().First();

            Console.WriteLine("Answer A: " + largestArea);

        }

        public int GetManhattanDistance(int x1, int x2, int y1, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

    }
}
