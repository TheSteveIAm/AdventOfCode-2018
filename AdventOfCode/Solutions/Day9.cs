using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    class CircularList<T> : List<T>
    {
        public new T this[int index]
        {
            get { return base[index % Count]; }
            set { base[index % Count] = value; }
        }

        public T this[T item, int distance]
        {
            get
            {
                var index = IndexOf(item);
                return this[index + distance];
            }
            set
            {
                var index = IndexOf(item);
                this[index + distance] = value;
            }
        }
    }

    class Marble
    {
        public Marble leftNeighbor;
        public Marble rightNeighbor;
        public int number;
    }

    //class Player
    //{
    //    public int playerNumber;
    //    public int score;
    //}

    class Day9
    {
        //just going to write the input in since it's 2 numbers...
        private const int playerCount = 458;
        private const int lastMarbleScore = 71307; //score earned from the last played marble
        private const int counterClockwiseMarbleDistance = 7; //how many marbles back from the current marble to also score
        private const int scoreMarble = 23; //score a marble if it's every 23rd marble
        //wrap around formula goes as (marbles.Count - 1) - (7 - currentMarble)

        List<int> marbles = new List<int>();
        int currentMarble = 0;
        int[] players = new int[playerCount];
        int lastScoredMarbleWorth = 0;
        int marbleNumber = 0;
        int playerIndex = 0;

        public int WrapIndex(int index, int count)
        {
            //replace this with a min or max or something
            if (count == 0)
            {
                return 0;
            }
            else if( count == 1)
            {
                return index % (count + 1);
            }
            else if (index < 0)
            {
                //wrap to top
                index = count - index;
            }
            else
            {
                index = index % count;
                //wrap to bottom
            }
            return index;
        }

        public void GetAnswerA()
        {
            Console.WriteLine("Answer A: ");

            //Starter marble
            marbles.Add(marbleNumber);

            //probably just...
            while (lastScoredMarbleWorth != 71307)
            {
                marbleNumber++;

                //score a marble!
                if (marbleNumber % scoreMarble == 0)
                {
                    players[playerIndex] += marbleNumber;
                    int sevenToTheLeft = WrapIndex(marbleNumber - 7, marbles.Count);
                    players[playerIndex] += sevenToTheLeft;
                    lastScoredMarbleWorth = marbles[sevenToTheLeft];

                    currentMarble = marbles[WrapIndex(sevenToTheLeft + 1, marbles.Count)];
                    marbles.RemoveAt(sevenToTheLeft);
                }
                else
                {
                    marbles.Insert(WrapIndex(currentMarble + 1, marbles.Count), marbleNumber);
                    currentMarble = marbleNumber;
                }
                playerIndex = WrapIndex(playerIndex + 1, players.Length);


                if (marbleNumber > 1000000)
                {
                    Console.WriteLine("We've done 1,000,000 marbles, we've probably gone too far!");
                    break;
                }
            }

        }



        public void GetAnswerB()
        {
            Console.WriteLine("Answer B: ");
        }


    }
}
