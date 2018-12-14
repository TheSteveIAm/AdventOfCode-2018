using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    class Node
    {
        public int nodeId;
        public int childrenCount;
        public int metaDataCount;
        public List<Node> children;
        public List<int> metaData;
        public Node parent;
        //public int nodeValue;

        public Node()
        {
            children = new List<Node>();
            metaData = new List<int>();
        }

        public int CalcValue()
        {
            int value = 0;

            if (childrenCount > 0)
            {
                //Console.WriteLine("Node: )F
                for (int i = 0; i < metaDataCount; i++)
                {
                    if (metaData[i] > 0 && metaData[i] <= childrenCount)
                    {
                        int reference = metaData[i] - 1;
                        value += children[reference].CalcValue();
                    }
                }
            }
            else
            {
                value = metaData.Sum();
            }
            return value;
        }
    }

    class Day8
    {
        private string path = "../../Input/input8.txt";
        bool parsed = false;
        string line;
        Queue<int> inputQueue = new Queue<int>();
        List<Node> nodeTree = new List<Node>();
        int metaDataSum = 0;
        int rootValue = 0;

        private void ParseInput(string path)
        {
            if (parsed)
                return;

            StreamReader reader = new StreamReader(path);

            while ((line = reader.ReadLine()) != null)
            {
                string[] stringSplit = line.Split(' ').ToArray();

                for (int i = 0; i < stringSplit.Length; i++)
                {
                    inputQueue.Enqueue(int.Parse(stringSplit[i]));
                }
            }

            reader.Close();

            parsed = true;
        }

        public void GetAnswerA()
        {
            Console.WriteLine("Answer A: ");
            ParseInput(path);

            Node currentNode, childNode;

            //create root node
            currentNode = new Node()
            {
                childrenCount = inputQueue.Dequeue(),
                metaDataCount = inputQueue.Dequeue(),
                nodeId = nodeTree.Count
            };

            nodeTree.Add(currentNode);

            while (inputQueue.Count > 1)
            {
                while (currentNode.children.Count < currentNode.childrenCount)
                {
                    childNode = new Node()
                    {
                        childrenCount = inputQueue.Dequeue(),
                        metaDataCount = inputQueue.Dequeue(),
                        parent = currentNode,
                        nodeId = nodeTree.Count
                    };

                    nodeTree.Add(childNode);

                    currentNode.children.Add(childNode);

                    currentNode = childNode;
                }

                if (currentNode.metaDataCount > currentNode.metaData.Count)
                {
                    for (int i = 0; i < currentNode.metaDataCount; i++)
                    {
                        int metaData = inputQueue.Dequeue();
                        metaDataSum += metaData;
                        currentNode.metaData.Add(metaData);
                    }
                }

                currentNode = currentNode.parent;
            }
            Console.WriteLine(metaDataSum);
        }

        public void GetAnswerB()
        {
            Console.WriteLine("Answer B: ");

            rootValue = nodeTree[0].CalcValue();

            Console.WriteLine(rootValue);
        }


    }
}
