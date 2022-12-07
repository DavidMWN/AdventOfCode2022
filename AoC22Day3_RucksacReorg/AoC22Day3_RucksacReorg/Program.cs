using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AoC22Day3_RucksacReorg
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1

            int sumPriorities = 0;
            bool continueCompare = true;

            Priorities priorityReference = new Priorities();

            string filePath = "../../../RucksackInput.txt";

            List<string> lines = new List<string>();
            lines = File.ReadAllLines(filePath).ToList();

            foreach (string l in lines)
            {
                continueCompare = true;

                for (int i = 0; i < (l.Length/2); i++)
                {
                    for (int x = (l.Length/2); x < l.Length; x++)
                    {
                        if (l[i] == l[x])
                        {
                            if (priorityReference.GetPriority(l[i]) > 0)
                                sumPriorities += priorityReference.GetPriority(l[i]);
                            else
                            {
                                Console.WriteLine("Error!");
                                return;
                            }
                            continueCompare = false;
                            break;
                        }
                    }

                    if (!continueCompare)
                        break;
                }
            }

            Console.WriteLine(sumPriorities);

            // Part 2

            BadgeFinder badgeFinder = new BadgeFinder();
            char badge;
            int sumBadgePriorities = 0;

            for (int i = 0; i < lines.Count; i+=3)
            {
                badgeFinder.SetElfGroup(lines[i], lines[i + 1], lines[i + 2]);
                badge = badgeFinder.FindBadge();

                sumBadgePriorities += priorityReference.GetPriority(badge);
            }

            Console.WriteLine(sumBadgePriorities);
        }
    }
}
