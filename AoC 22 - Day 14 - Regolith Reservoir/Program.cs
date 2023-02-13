using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoCDa22Day14_RegolithReservoir
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1

            List<string> puzzleInput = new List<string>();

            puzzleInput = File.ReadAllLines("../../../PuzzleRockPaths.txt").ToList();

            RockScan rockScan = new RockScan();

            List<int> xCoordinates = new List<int>();
            List<int> yCoordinates = new List<int>();

            int minXCoordinate = 500;
            int maxYCoordinate = 0;

            foreach (string line in puzzleInput)
            {
                string[] parse = line.Split(' ');

                foreach (string section in parse)
                {
                    if (Char.IsDigit(section[0]))
                    {
                        string[] input = section.Split(',');

                        xCoordinates.Add(Convert.ToInt32(input[0]));
                        yCoordinates.Add(Convert.ToInt32(input[1]));
                    }
                }

                for (int i = 0; i < xCoordinates.Count - 1; i++)
                {
                    rockScan.SetRockPath(xCoordinates[i], yCoordinates[i], xCoordinates[i + 1], yCoordinates[i + 1]);
                }

                xCoordinates.Sort();

                if (xCoordinates[0] < minXCoordinate)
                    minXCoordinate = xCoordinates[0];

                xCoordinates.Clear();

                yCoordinates.Sort();

                if (yCoordinates[yCoordinates.Count - 1] > maxYCoordinate)
                    maxYCoordinate = yCoordinates[yCoordinates.Count - 1];

                yCoordinates.Clear();
            }

            rockScan.TestPrint(minXCoordinate, maxYCoordinate);

            Console.WriteLine("\n\n");

            int sandCount = 0;
            bool abyssCheck = false;

            while (abyssCheck == false)
            {
                if (rockScan.SandFallAbyss())
                    sandCount++;
                else
                    abyssCheck = true;
            }

            rockScan.TestPrint(minXCoordinate, maxYCoordinate);

            Console.WriteLine("\n\n" + sandCount + "\n\n");

            // Part 2

            RockScan rockScan2 = new RockScan();

            int minXCoordinate2 = 500;
            int maxYCoordinate2 = 0;

            foreach (string line in puzzleInput)
            {
                string[] parse = line.Split(' ');

                foreach (string section in parse)
                {
                    if (Char.IsDigit(section[0]))
                    {
                        string[] input = section.Split(',');

                        xCoordinates.Add(Convert.ToInt32(input[0]));
                        yCoordinates.Add(Convert.ToInt32(input[1]));
                    }
                }

                for (int i = 0; i < xCoordinates.Count - 1; i++)
                {
                    rockScan2.SetRockPath(xCoordinates[i], yCoordinates[i], xCoordinates[i + 1], yCoordinates[i + 1]);
                }

                xCoordinates.Sort();

                if (xCoordinates[0] < minXCoordinate2)
                    minXCoordinate2 = xCoordinates[0];

                xCoordinates.Clear();

                yCoordinates.Sort();

                if (yCoordinates[yCoordinates.Count - 1] > maxYCoordinate2)
                    maxYCoordinate2 = yCoordinates[yCoordinates.Count - 1];

                yCoordinates.Clear();
            }

            maxYCoordinate2 += 2;

            rockScan2.SetRockFloor(maxYCoordinate2);

            rockScan2.TestPrint(minXCoordinate2, maxYCoordinate2);

            Console.WriteLine("\n\n");

            int sandCount2 = 0;
            bool blockCheck = false;

            while (blockCheck == false)
            {
                if (rockScan2.SandFallFloor())
                    sandCount2++;
                else
                {
                    sandCount2++;
                    blockCheck = true;
                }
            }

            rockScan2.TestPrint(minXCoordinate2, maxYCoordinate2);

            Console.WriteLine("\n\n" + sandCount2 + "\n\n");
        }
    }
}
