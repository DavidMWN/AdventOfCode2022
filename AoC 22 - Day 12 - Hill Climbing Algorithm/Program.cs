using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AoC22Day12_HillClimbingAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1

            List<string> puzzleInput = new List<string>();
            puzzleInput = File.ReadAllLines("../../../ElevationInput.txt").ToList();

            List<List<char>> elevationGrid = new List<List<char>>();

            int xStart = 0;
            int yStart = 0;

            int shortestPath;

            for (int i = 0; i < puzzleInput.Count(); i++)
            {
                elevationGrid.Add(new List<char>());

                for (int c = 0; c < puzzleInput[i].Count(); c++)
                {
                    if (puzzleInput[i][c] == 'S')
                    {
                        yStart = i;
                        xStart = c;                        
                    }
                    elevationGrid[i].Add(puzzleInput[i][c]);
                }
            }

            ShortestPathFinder shortestPathFinder = new ShortestPathFinder(elevationGrid);

            shortestPath = shortestPathFinder.FindPath(xStart, yStart);

            Console.WriteLine(shortestPath);

            // Part 2

            List<int> hikePaths = new List<int>();
            int shortestHikePath;
            int tempStepCount;

            elevationGrid[yStart][xStart] = 'a';

            ShortestPathFinder shortestHikeFinder = new ShortestPathFinder(elevationGrid);            

            for (int y = 0; y < elevationGrid.Count; y++)
            {
                for (int x = 0; x < elevationGrid[y].Count; x++)
                {
                    if (elevationGrid[y][x] == 'a')
                    {
                        tempStepCount = shortestHikeFinder.FindPath(x, y);

                        if (tempStepCount != -1)
                            hikePaths.Add(tempStepCount);
                        shortestHikeFinder.resetFinder();
                    }
                }
            }

            hikePaths.Sort();

            shortestHikePath = hikePaths[0];

            Console.WriteLine(shortestHikePath);
        }
    }
}
