using System;
using System.Collections.Generic;

namespace AoCDa22Day14_RegolithReservoir
{
    class RockScan
    {
        List<List<char>> grid;

        private int sandStartOffset = 0;

        public RockScan()
        {
            grid = new List<List<char>>();

            grid.Add(new List<char>());

            grid[0].Add('.');
        }

        public void SetRockPath(int x1, int y1, int x2, int y2)
        {
            int maxXIndex = Math.Max(x1, x2) + 2;
            int maxYIndex = Math.Max(y1, y2) + 2;

            if (grid.Count < maxYIndex + 1)
            {
                for (int y = grid.Count; y < maxYIndex + 1; y++)
                {
                    grid.Add(new List<char>());

                    for (int x = 0; x < grid[0].Count; x++)
                        grid[grid.Count - 1].Add('.');
                }
            }

            if (grid[0].Count < maxXIndex + 1)
            {
                for (int y = 0; y < grid.Count; y++)
                {
                    for (int x = grid[y].Count; x < maxXIndex + 1; x++)
                        grid[y].Add('.');
                }                    
            }

            // Drawing a path horizontal left
            if (x1 > x2)
            {
                for (int i = x1; i >= x2; i--)
                {
                    grid[y1][i] = '#';
                }
            }

            // Drawing a path horizontal right
            if (x1 < x2)
            {
                for (int i = x1; i <= x2; i++)
                {
                    grid[y1][i] = '#';
                }
            }

            // Drawing a path vertical up
            if (y1 > y2)
            {
                for (int i = y1; i >= y2; i--)
                {
                    grid[i][x1] = '#';
                }
            }

            // Drawing a path vertical down
            if (y1 < y2)
            {
                for (int i = y1; i <= y2; i++)
                {
                    grid[i][x1] = '#';
                }
            }
        }

        public void SetRockFloor(int maxYIndex)
        {
            if (grid.Count < maxYIndex + 1)
            {
                for (int y = grid.Count; y < maxYIndex + 1; y++)
                {
                    grid.Add(new List<char>());

                    for (int x = 0; x < grid[0].Count; x++)
                        grid[grid.Count - 1].Add('.');
                }
            }

            for (int x = 0; x < grid[0].Count; x++)
            {
                grid[maxYIndex][x] = '#';
            }
        }

        public void TestPrint(int minX, int MaxY)
        {
            for (int y = 0; y <= MaxY; y++)
            {
                if (sandStartOffset == 0)
                {
                    for (int x = minX - 2; x < grid[y].Count; x++)
                    {
                        Console.Write(grid[y][x]);
                    }
                }
                else
                {
                    for (int x = 0; x < grid[y].Count; x++)
                        Console.Write(grid[y][x]);
                }

                Console.Write("\n");
            }
        }

        public bool SandFallAbyss()
        {
            int x = 500 + sandStartOffset;
            int y = 0;

            while (y < grid.Count - 1)
            {
                if (DownCheck(x, y))
                {
                    y++;
                    continue;
                }
                
                if (DownLeftCheck(x, y))
                {
                    y++;
                    x--;
                    continue;
                }

                if (DownRightCheck(x, y))
                {
                    y++;
                    x++;
                    continue;
                }

                break;
            }

            if (y + 1 == grid.Count)
                return false;

            grid[y][x] = 'O';

            return true;
        }

        public bool SandFallFloor()
        {
            int x = 500 + sandStartOffset;
            int y = 0;

            bool blocked = false;

            while (blocked == false)
            {
                GridExpansionCheck(x);

                if (DownCheck(x, y))
                {
                    y++;
                    continue;
                }

                if (DownLeftCheck(x, y))
                {
                    y++;
                    x--;
                    continue;
                }

                if (DownRightCheck(x, y))
                {
                    y++;
                    x++;
                    continue;
                }

                blocked = true;
            }

            grid[y][x] = 'O';

            if (grid[0][500 + sandStartOffset] == 'O')
                return false;
            else
                return true;
        }

        private bool DownCheck(int x, int y)
        {
            if (grid[y + 1][x] == '.')
                return true;
            else
                return false;
        }

        private bool DownLeftCheck(int x, int y)
        {
            if (grid[y + 1][x - 1] == '.')
                return true;
            else
                return false;            
        }

        private bool DownRightCheck(int x, int y)
        {
            if (grid[y + 1][x + 1] == '.')
                return true;
            else
                return false;           
        }

        private void GridExpansionCheck(int nextX)
        {
            if (nextX < 0)
            {
                foreach (List<char> c in grid)
                {
                    c.Insert(0, '.');
                }

                sandStartOffset++;
            }

            if (grid[0].Count <= nextX + 1)
            {
                for (int y = 0; y < grid.Count; y++)
                {
                    if (y == grid.Count - 1)
                        grid[y].Add('#');
                    else
                        grid[y].Add('.');
                }
            }
        }
    }
}
