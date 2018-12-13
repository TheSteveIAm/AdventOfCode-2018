using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    class Step
    {
        public char stepId;
        public List<char> stepPrerequisites = new List<char>();
        public int stepTime;
    }

    class Worker
    {
        public Step step;

        public void GetStep(List<KeyValuePair<char, Step>> orderedSteps)
        {
            for (int i = 0; i < orderedSteps.Count; i++)
            {
                if (orderedSteps[i].Value.stepPrerequisites.Count == 0)
                {
                    step = orderedSteps[i].Value;
                    orderedSteps.RemoveAt(i);
                    break;
                }
            }
        }
    }

    class Day7
    {
        private string path = "../../Input/input7.txt";
        bool parsed = false;
        string line;
        public Dictionary<char, Step> steps = new Dictionary<char, Step>();
        int requirementCount = 0;

        private void ParseInput(string path)
        {
            if (parsed)
                return;

            StreamReader reader = new StreamReader(path);

            for (int i = 65; i < 91; i++)
            {
                char stepId = char.ConvertFromUtf32(i).ToCharArray()[0];
                steps.Add(stepId, new Step());
                int time = i - 4;
                steps[stepId].stepTime = time;
                steps[stepId].stepId = stepId;
            }

            while ((line = reader.ReadLine()) != null)
            {
                char[] stringSplit = line.ToCharArray();

                steps[stringSplit[36]].stepPrerequisites.Add(stringSplit[5]);
                requirementCount++;
            }

            reader.Close();

            parsed = true;
        }

        public void GetAnswerB()
        {
            Console.WriteLine("Answer B: ");
            ParseInput(path);

            var orderedSteps = steps.OrderBy(x => x.Key).ToList();
            Worker[] workers = new Worker[5];

            for (int i = 0; i < workers.Length; i++)
            {
                workers[i] = new Worker();
            }

            int time = 0;

            bool waitFlag = false;
            while (orderedSteps.Count > 0)
            {
                for (int j = 0; j < workers.Length; j++)
                {
                    Worker w = workers[j];
                    if (w.step == null && !waitFlag)
                    {
                        //find the next step to work on
                        w.GetStep(orderedSteps);
                    }

                    if (w.step != null)
                    {
                        //process step
                        w.step.stepTime--;
                        //Console.Write(string.Format("{0} ", w.step.stepId));

                        if (w.step.stepTime < 1)
                        {
                            for (int i = 0; i < orderedSteps.Count; i++)
                            {
                                if (orderedSteps[i].Value.stepPrerequisites.Contains(w.step.stepId))
                                    requirementCount--;

                                orderedSteps[i].Value.stepPrerequisites.Remove(w.step.stepId);
                            }

                            w.step = null;
                            w.GetStep(orderedSteps);
                            waitFlag = true;
                        }
                    }
                    //else
                    //{
                    //    Console.Write("- ");
                    //}
                }
                time++;
                //Console.Write("\n");
                waitFlag = false;
            }

            //get any remaining time after initial calculate, because it ends before allowing workers to finish their task
            foreach (Worker w in workers)
            {
                while (w.step != null)
                {
                    time++;
                    w.step.stepTime--;
                    //Console.WriteLine(string.Format("{0} ", w.step.stepId));
                    if (w.step.stepTime < 1)
                        w.step = null;
                }
            }

            Console.WriteLine("Time: " + time);
        }

        public void GetAnswerA()
        {
            Console.WriteLine("Answer A: ");
            ParseInput(path);
            var orderedSteps = steps.OrderBy(x => x.Key).ToList();

            while (orderedSteps.Count > 0)
            {
                for (int i = 0; i < orderedSteps.Count; i++)
                {
                    if (orderedSteps[i].Value.stepPrerequisites.Count == 0)
                    {
                        Console.Write(orderedSteps[i].Key);

                        foreach (var s in orderedSteps)
                        {
                            if (s.Value.stepPrerequisites.Contains(orderedSteps[i].Key))
                                requirementCount--;

                            s.Value.stepPrerequisites.Remove(orderedSteps[i].Key);
                        }

                        orderedSteps.RemoveAt(i);

                        break;
                    }
                }
            }
            Console.Write("\n");
        }

    }
}
