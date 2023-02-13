using System.Collections.Generic;

namespace AoC22Day12_HillClimbingAlgorithm
{
    class VisitedNodeTracker
    {       
        private List<List<bool>> visited;

        public VisitedNodeTracker(int xCount, int yCount)
        {
            visited = new List<List<bool>>();
            
            // Creates a grid and sets all values in the grid to false;
            for (int y = 0; y < yCount; y++)
            {
                visited.Add(new List<bool>());

                for (int x = 0; x < xCount; x++)
                {
                    visited[y].Add(false);
                }
            }
        }

        public void MarkVisited(int x, int y)
        {
            visited[y][x] = true;
        }

        public bool CheckIfVisited(int x, int y)
        {
            return visited[y][x];
        }

        // Resets values of every node on the grid to false, for Part 2 when multiple searches are run in succession
        public void ResetTracker()
        {
            for (int y = 0; y < visited.Count; y++)
            {
                for (int x = 0; x < visited[y].Count; x++)
                {
                    visited[y][x] = false;
                }
            }
        }
    }
}
