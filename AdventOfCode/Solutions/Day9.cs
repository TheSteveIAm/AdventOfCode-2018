using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    class Day9
    {
        //just going to write the input in since it's 2 numbers...
        private const int playerCount = 458;
        private const int lastMarbleScore = 71307; //score earned from the last played marble
        private const int counterClockwiseMarbleDistance = 7; //how many marbles back from the current marble to also score
        private const int scoreMarble = 23; //score a marble if it's every 23rd marble
        //wrap around formula goes as (marbles.Count - 1) - (7 - currentMarble)

        List<int> marbles = new List<int>();
        private int currentMarble = 0;
        int[] players = new int[playerCount];
        int lastScoredMarbleWorth = 0;
        int marbleNumber = 0;

        public int WrapIndex(int index, int count)
        {
            //replace this with a min or max or something
            if(marbles.Count == 0)
            {
                return 0;
            }

            if(index < 0)
            {
                //wrap to top
                index = (marbles.Count - 1) - (index);
            }
            else
            {
                
                index = (marbles.Count - (marbles.Count + count)) - 1;
                //wrap to bottom
                //index = (marbles.Count - 1) - (index + count);
            }
            return index;
        }

        public void GetAnswerA()
        {
            Console.WriteLine("Answer A: ");
            //marbles.Insert();
            //marbles.RemoveAt();
            //you'll be using i+1 mod 23 = 0 for this



            //probably just...
            while (lastScoredMarbleWorth != 71307)
            {
                //score a marble!
                if (marbleNumber + 1 % scoreMarble == 0)
                {

                }
                else
                {
                    marbles.Insert(WrapIndex(currentMarble + 1), marbleNumber);
                }
            }
        }

        public void GetAnswerB()
        {
            Console.WriteLine("Answer B: ");
        }


    }
}
