using System;
using System.Collections.Generic;

namespace AoC22Day12_HillClimbingAlgorithm
{
    class ShortestPathFinder
    {
        private List<List<char>> grid;

        private int xCoord;
        private int yCoord;

        // Queues used for Breadth First Search
        private List<int> xQueue = new List<int>();
        private List<int> yQueue = new List<int>();

        private int stepCount;
        private int nodesLeftInLayer = 1;
        private int nodesInNextLayer = 0;

        private bool endReached = false;

        private VisitedNodeTracker visitedTracker;

        private int[] xDirectionVectors = { 0, 0, 1, -1 };
        private int[] yDirectionVectors = { -1, 1, 0, 0 };

        public ShortestPathFinder(List<List<char>> elevationGrid)
        {
            grid = new List<List<char>>(elevationGrid);

            visitedTracker = new VisitedNodeTracker(elevationGrid[0].Count, elevationGrid.Count);
        }

        public int FindPath(int xStart, int yStart)
        {
            // Initializes starting point
            xQueue.Add(xStart);
            yQueue.Add(yStart);
            visitedTracker.MarkVisited(xStart, yStart);

            // Change starting point value from 'S' to 'a' (its elevation level) to avoid extraneous logic comparisons
            grid[yStart][xStart] = 'a';

            while (yQueue.Count > 0)
            {
                // Sets current coordinates/current position
                xCoord = xQueue[0];
                yCoord = yQueue[0];

                // Checks if current position is the endpoint
                if (grid[yCoord][xCoord] == 'E')
                {
                    endReached = true;
                    break;
                }

                // Checks surrounding nodes (excluding diagonals)
                exploreSurroundingNodes(xCoord, yCoord);

                // Manages the current "layer" of nodes being searched
                nodesLeftInLayer--;

                // When finished with the current "layer" of nodes, one step is completed, moves to next "layer"
                if (nodesLeftInLayer == 0)
                {
                    nodesLeftInLayer = nodesInNextLayer;
                    nodesInNextLayer = 0;
                    stepCount++;
                }

                // Takes current position coordinates off of the piles of nodes to be searched
                xQueue.RemoveAt(0);
                yQueue.RemoveAt(0);
            }

            // Returns total steps taken if the ending point is reached
            if (endReached)
                return stepCount;

            // Returns -1 if no path reached the ending point
            return -1;
        }

        private void exploreSurroundingNodes(int x, int y)
        {
            // Temp values to hold coordinates of nodes to be searched
            int row;
            int col;

            // Searches the nodes above, below, right, and left of the current node
            for (int i = 0; i < 4; i++)
            {
                // Sets temp values to coordinates of node to be evaluated
                row = y + yDirectionVectors[i];
                col = x + xDirectionVectors[i];

                // Check for out of bounds
                if (row < 0 || col < 0)
                    continue;
                if (row >= grid.Count || col >= grid[0].Count)
                    continue;

                // Check if the node has already been visited
                if (visitedTracker.CheckIfVisited(col, row))
                    continue;

                // Check elevation (where end point E is at elevation 'z', and you can only step 1 elevation higher, but any elevation lower)
                if (grid[row][col] == 'E')
                {
                    if (grid[y][x] != 'z')
                        if (grid[y][x] != 'y')
                            continue;
                }
                else if (grid[row][col] > grid[y][x] + 1)
                {
                    continue;
                }

                // Add to the search queue if all conditions are satisfied
                yQueue.Add(row);
                xQueue.Add(col);

                // Mark the node as visited to prevent being added to the queue multiple times
                visitedTracker.MarkVisited(col, row);

                // Adds to count of the next "Layer" of nodes to search
                nodesInNextLayer++;
            }
        }

        // Reseting relevent values to starting points for Part 2, when it needs to be run several times in succession
        public void resetFinder()
        {
            stepCount = 0;
            endReached = false;

            nodesLeftInLayer = 1;
            nodesInNextLayer = 0;

            xQueue.Clear();
            yQueue.Clear();

            visitedTracker.ResetTracker();
        }
    }
}
