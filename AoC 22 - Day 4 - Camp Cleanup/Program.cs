using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AoC22Day4_CampCleanup
{
    class Program
    {
        static void Main(string[] args)
        {

            // Part 1

            string filePath = "../../../CleanupAssignments.txt";

            List<string> lines = new List<string>();
            lines = File.ReadAllLines(filePath).ToList();

            string[] elfPair = new string[2];
            string[] elf1Assignments = new string[2];
            string[] elf2Assignments = new string[2];
            bool hasFullyContained;
            int sumFullyContained = 0;

            foreach (string l in lines)
            {
                hasFullyContained = false;

                elfPair = l.Split(',');
                elf1Assignments = elfPair[0].Split('-');
                elf2Assignments = elfPair[1].Split('-');

                if ((Convert.ToInt32(elf1Assignments[0]) <= Convert.ToInt32(elf2Assignments[0])) && (Convert.ToInt32(elf1Assignments[1]) >= Convert.ToInt32(elf2Assignments[1])))
                    hasFullyContained = true;
                
                if (!hasFullyContained)
                {
                    if ((Convert.ToInt32(elf2Assignments[0]) <= Convert.ToInt32(elf1Assignments[0])) && (Convert.ToInt32(elf2Assignments[1]) >= Convert.ToInt32(elf1Assignments[1])))
                    {
                        hasFullyContained = true;
                    }
                }

                if (hasFullyContained)
                    sumFullyContained++;
            }

            Console.WriteLine(sumFullyContained);

            // Part 2

            int sumOverlap = 0;
            bool hasOverlap;

            foreach (string l in lines)
            {
                hasOverlap = false;

                elfPair = l.Split(',');
                elf1Assignments = elfPair[0].Split('-');
                elf2Assignments = elfPair[1].Split('-');

                if (Convert.ToInt32(elf2Assignments[0]) <= Convert.ToInt32(elf1Assignments[0]) && Convert.ToInt32(elf1Assignments[0]) <= Convert.ToInt32(elf2Assignments[1]))
                    hasOverlap = true;

                if (Convert.ToInt32(elf2Assignments[0]) <= Convert.ToInt32(elf1Assignments[1]) && Convert.ToInt32(elf1Assignments[1]) <= Convert.ToInt32(elf2Assignments[1]))
                    hasOverlap = true;

                if (Convert.ToInt32(elf1Assignments[0]) <= Convert.ToInt32(elf2Assignments[0]) && Convert.ToInt32(elf2Assignments[0]) <= Convert.ToInt32(elf1Assignments[1]))
                    hasOverlap = true;

                if (Convert.ToInt32(elf1Assignments[0]) <= Convert.ToInt32(elf2Assignments[1]) && Convert.ToInt32(elf2Assignments[1]) <= Convert.ToInt32(elf1Assignments[1]))
                    hasOverlap = true;

                if (hasOverlap)
                    sumOverlap++;
            }                       

            Console.WriteLine(sumOverlap);
        }
    }
}
