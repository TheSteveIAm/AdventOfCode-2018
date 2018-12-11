using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    public struct GuardLog
    {
        public DateTime date;
        public string entry;

    }

    public class GuardProfile
    {
        public int id;
        public int minutesSlept;
        public List<int> rangeAsleep;
        public int sleepiestMinute;
        public int numberOfTimesAsleepAtSleepiestMinute;
    }

    class Day4
    {

        //private Tuple<int, DateTime[], int[]> shiftData;
        //private Dictionary<int, DateTime> shifts = new Dictionary<int, DateTime>(); //id, datetime
        private string path = "../../Input/input4.txt";

        //private List<string> lines = new List<string>();
        private HashSet<GuardLog> logs = new HashSet<GuardLog>();
        private List<GuardProfile> profiles = new List<GuardProfile>();

        bool parsed = false;

        public void ParseInput(string path)
        {
            if (parsed)
                return;

            StreamReader reader = new StreamReader(path);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                //lines.Add(line.Trim());

                string[] stringSplit = line.Split(']');
                DateTime dt;
                DateTime.TryParse(stringSplit[0].Trim('['), out dt);

                GuardLog log = new GuardLog
                {
                    date = dt,
                    entry = stringSplit[1].Trim()
                };

                //Console.WriteLine(dt + " : " + log.entry);

                logs.Add(log);
            }

            reader.Close();

            var sortedLogs = logs.OrderBy(logDate => logDate.date);

            int id = -1;
            int minAsleep = -1;
            int minAwake = -1;

            foreach (GuardLog log in sortedLogs)
            {

                //Console.WriteLine(log.date + " : " + log.entry);
                if (log.entry.Contains('#'))
                {
                    id = int.Parse(log.entry.Split('#')[1].Split(' ')[0]);

                    if (profiles.All(p => p.id != id))
                    {
                        GuardProfile guard = new GuardProfile
                        {
                            id = id,
                            rangeAsleep = new List<int>()
                        };

                        profiles.Add(guard);
                    }

                }
                else if (log.entry.Contains("asleep"))
                {
                    minAsleep = log.date.Minute;
                }
                else if (log.entry.Contains("wakes"))
                {
                    if (id == -1 || minAsleep == -1)
                    {
                        throw new ArgumentException("Order of logs is wrong");
                    }

                    minAwake = log.date.Minute;

                    GuardProfile guard = profiles.First(g => g.id == id);

                    int sleepRange = (minAwake - minAsleep);
                    guard.minutesSlept += sleepRange;
                    guard.rangeAsleep.AddRange(Enumerable.Range(minAsleep, sleepRange));

                    minAsleep = -1;
                    minAwake = -1;
                }
            }
            parsed = true;
        }

        public void GetAnswer()
        {
            ParseInput(path);


            foreach (GuardProfile g in profiles)
            {
                g.sleepiestMinute = g.rangeAsleep.GroupBy(i => i)
                    .OrderByDescending(m => m.Count())
                    .Select(m => m.Key)
                    .FirstOrDefault();

                g.numberOfTimesAsleepAtSleepiestMinute = g.rangeAsleep.GroupBy(i => i)
                    .OrderByDescending(m => m.Count())
                    .Select(m => m.Count())
                    .FirstOrDefault();
            }

            List<GuardProfile> sortedBySleep = profiles.OrderByDescending(g => g.minutesSlept).ToList();
            List<GuardProfile> sortedByConsistency = profiles.OrderByDescending(g => g.numberOfTimesAsleepAtSleepiestMinute).ToList();

            Console.WriteLine("Guard: " + sortedBySleep[0].id + " slept " + sortedBySleep[0].minutesSlept + " minutes");
            Console.WriteLine("Minute: " + sortedBySleep[0].sleepiestMinute);
            Console.WriteLine("Guard # * sleepiest minute: " + (sortedBySleep[0].id * sortedBySleep[0].sleepiestMinute));
            Console.WriteLine("Guard # " + sortedByConsistency[0].id + " fell asleep at minute " + sortedByConsistency[0].sleepiestMinute + " " + sortedByConsistency[0].numberOfTimesAsleepAtSleepiestMinute + " times " +
                " giving a multiplied result of: " + (sortedByConsistency[0].id * sortedByConsistency[0].sleepiestMinute));

            //List<int> sortedMinutes = sortedBySleep[0].rangeAsleep.GroupBy(i => i)
            //    .OrderByDescending(m => m.Count())
            //    .Select(m => m.Key).ToList();

            //foreach (int i in sortedMinutes)
            //{
            //    Console.WriteLine(i);
            //}

            //foreach (int i in sortedBySleep[0].rangeAsleep)
            //{
            //    Console.WriteLine(i);
            //}

            //foreach (GuardProfile guard in profiles)
            //{
            //    Console.WriteLine("Guard # " + guard.id + " has slept " + guard.minutesSlept);
            //}
        }

        //public void GetAnswerB()
        //{
        //ParseInput(path);

        //        List<GuardProfile> consistentGuards = profiles.GroupBy(g => g)
        //.OrderByDescending()
        //}
    }
}
