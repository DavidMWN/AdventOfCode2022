using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AoC22Day8_TreetopTreeHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1

            string filePath = "../../../TreeHeightGrid.txt";

            List<string> lines = new List<string>();
            lines = File.ReadAllLines(filePath).ToList();

            List<List<int>> treeHeightGrid = new List<List<int>>();

            for (int i = 0; i < lines.Count(); i++)
            {
                treeHeightGrid.Add(new List<int>());

                foreach (char c in lines[i])
                {
                    treeHeightGrid[i].Add(int.Parse(c.ToString()));
                }
            }

            // Add the total number of trees in the first and last rows, because all of them are visible by default.
            int visibleTrees = treeHeightGrid[0].Count() + treeHeightGrid[treeHeightGrid.Count() - 1].Count();

            // Add the first and last tree in each row by taking the total number of rows (minus two so as not to double-count the trees in the first and last rows) and multipying by 2.
            visibleTrees += (treeHeightGrid.Count() - 2) * 2;

            // Count visible trees in every row except first and last rows, and except first and last columns
            for (int i = 1; i < treeHeightGrid.Count() - 1; i++)
            {
                visibleTrees += CountVisibleTrees(treeHeightGrid, treeHeightGrid[i]);
            }

            Console.WriteLine(visibleTrees);

            // Part 2

            int maxScenicScore = 0;
            int tempScore = 0;

            for (int i = 1; i < treeHeightGrid.Count() - 1; i++)
            {
                tempScore = FindRowHighScenicScore(treeHeightGrid, treeHeightGrid[i]);

                if (tempScore > maxScenicScore)
                    maxScenicScore = tempScore;
            }

            Console.WriteLine(maxScenicScore);
        }

        static int CountVisibleTrees(List<List<int>> treeGrid, List<int> treeRow)
        {
            int visibleTreeCount = 0;

            bool treeVisible;

            // Iterate through every tree in the row, except the first and last
            for (int x = 1; x < treeRow.Count() - 1; x++)
            {
                treeVisible = false;

                // Iterate through every tree in the same column above current tree, up to current tree
                for (int y = 0; y < treeGrid.IndexOf(treeRow); y++)
                {
                    // Check the column tree against the current row tree, breaks the loop if a blocking tree is found
                    if (treeGrid[y][x] >= treeRow[x])
                        break;

                    // If no tree has blocked visiblity of the current tree, it is visible
                    if (y == (treeGrid.IndexOf(treeRow) - 1))
                    {
                        visibleTreeCount++;
                        treeVisible = true;
                    }
                }

                // Moves to next tree if current tree is already found to be visible
                if (treeVisible)
                    continue;

                // Iterate through every tree in the same column below current tree
                for (int y = treeGrid.IndexOf(treeRow) + 1; y < treeGrid.Count(); y ++)
                {
                    // Check the column tree against the current row tree, breaks the loop if a blocking tree is found
                    if (treeGrid[y][x] >= treeRow[x])
                        break;

                    // If no tree has blocked visibility of the current tree, it is visible
                    if (y == (treeGrid.Count() - 1))
                    {
                        visibleTreeCount++;
                        treeVisible = true;
                    }
                }

                // Moves to next tree if current tree is already found to be visible
                if (treeVisible)
                    continue;

                // Iterate through every tree in the same row as current tree, up to current tree (that is, all trees to the left)
                for (int z = 0; z < x; z++)
                {
                    // Check the row tree against the current tree, breaks the loop if a blocking tree is found
                    if (treeRow[z] >= treeRow[x])
                        break;

                    // If no tree has blocked visibility of the current tree, it is visible
                    if (z == x - 1)
                    {
                        visibleTreeCount++;
                        treeVisible = true;
                    }
                }

                // Moves to next tree if current tree is already found to be visible
                if (treeVisible)
                    continue;

                // Iterate through every tree in the same row as current tree, after current tree (to the right of)
                for (int z = x + 1; z < treeRow.Count(); z++)
                {
                    // Check the row tree against the current tree, breaks the loop if a blocking tree is found
                    if (treeRow[z] >= treeRow[x])
                        break;

                    // If no tree has blocked visibility of the current tree, it is visible
                    if (z == (treeRow.Count() - 1))
                    {
                        visibleTreeCount++;
                    }
                }
            }
                        
            return visibleTreeCount;
        }

        static int FindRowHighScenicScore(List<List<int>> treeGrid, List<int> treeRow)
        {
            int visibleTreesUp ;
            int visibleTreesDown ;
            int visibleTreesLeft ;
            int visibleTreesRight ;

            int MaxRowScore = 0;
            int tempTreeScore;

            // Iterate through every tree in the row, except the first and last
            for (int x = 1; x < treeRow.Count() - 1; x++)
            {
                visibleTreesUp = 0;
                visibleTreesDown = 0;
                visibleTreesLeft = 0;
                visibleTreesRight = 0;
                tempTreeScore = 0;

                // Iterate through every tree in the same column above current tree
                for (int y = treeGrid.IndexOf(treeRow) - 1; y >= 0; y--)
                {
                    // Check the column tree against the current row tree, increases visible tree count if lower
                    if (treeGrid[y][x] < treeRow[x])
                        visibleTreesUp++;
                    else
                    {
                        // Adds last visible tree to count, breaks loop
                        visibleTreesUp++;
                        break;
                    }
                }

                // Iterate through every tree in the same column below current tree
                for (int y = treeGrid.IndexOf(treeRow) + 1; y < treeGrid.Count(); y++)
                {
                    // Check the column tree against the current row tree, increases visible tree count if lower
                    if (treeGrid[y][x] < treeRow[x])
                        visibleTreesDown++;
                    else
                    {
                        // Adds last visible tree to count, breaks loop
                        visibleTreesDown++;
                        break;
                    }
                }

                // Iterate through every tree in the same row as current tree, going left of current tree
                for (int z = x - 1; z >= 0; z--)
                {
                    // Check the row tree against the current tree, increases visible tree count if lower
                    if (treeRow[z] < treeRow[x])
                        visibleTreesLeft++;
                    else
                    {
                        // Adds last visible tree to count, breaks loop
                        visibleTreesLeft++;
                        break;
                    }
                }

                // Iterate through every tree in the same row as current tree, going right of current tree
                for (int z = x + 1; z < treeRow.Count(); z++)
                {
                    // Check the row tree against the current tree, increases visible tree count if loser
                    if (treeRow[z] < treeRow[x])
                        visibleTreesRight++;
                    else
                    {
                        // Adds last visible tree to count, breaks loop
                        visibleTreesRight++;
                        break;
                    }
                }

                tempTreeScore = visibleTreesUp * visibleTreesDown * visibleTreesLeft * visibleTreesRight;

                if (tempTreeScore > MaxRowScore)
                    MaxRowScore = tempTreeScore;
            }

            return MaxRowScore;
        }
    }
}
