using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AoC22Day7_NoSpaceLeft
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1

            string filePath = "../../../DirectoryInput.txt";

            List<string> lines = new List<string>();
            lines = File.ReadAllLines(filePath).ToList();

            int sumAllSmallDir = 0;
            
            List<int> tempDir = new List<int>();

            string[] tempData = new string[2];

            foreach (string l in lines)
            {
                if (Char.IsDigit(l[0]))
                {
                    tempData = l.Split(" ");

                    for (int i = 0; i < tempDir.Count(); i ++)
                        tempDir[i] += Convert.ToInt32(tempData[0]);

                    continue;
                }

                if (l == "$ ls")
                {
                    tempDir.Add(0);

                    continue;
                }

                if (l == "$ cd ..")
                {
                    if (tempDir[tempDir.Count() - 1] <= 100000)
                    {
                        sumAllSmallDir += tempDir[tempDir.Count() - 1];
                    }
                    tempDir.RemoveAt((tempDir.Count() - 1));
                }
            }

            Console.WriteLine(sumAllSmallDir);

            // Part 2

            int totalSpace = 70000000;
            int spaceNeeded = 30000000;
            int freeSpace = totalSpace - tempDir[0];
            List<int> deletionCandidates = new List<int>();

            tempDir.Clear();

            foreach (string l in lines)
            {
                if (Char.IsDigit(l[0]))
                {
                    tempData = l.Split(" ");

                    for (int i = 0; i < tempDir.Count(); i++)
                        tempDir[i] += Convert.ToInt32(tempData[0]);

                    continue;
                }

                if (l == "$ ls")
                {
                    tempDir.Add(0);

                    continue;
                }

                if (l == "$ cd ..")
                {
                    if (freeSpace + tempDir[tempDir.Count() - 1] >= spaceNeeded)
                    {
                        deletionCandidates.Add(tempDir[tempDir.Count() - 1]);
                    }
                    tempDir.RemoveAt((tempDir.Count() - 1));
                }
            }

            deletionCandidates.Sort();

            Console.WriteLine(deletionCandidates[0]);
        }
    }
}
