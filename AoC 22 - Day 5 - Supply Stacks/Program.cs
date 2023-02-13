using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AoC22Day5_SupplyStacks
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../../../StackRearrangementProcedure.txt";

            List<string> lines = new List<string>();
            lines = File.ReadLines(filePath).Skip(10).ToList();

            List<List<char>> stacks = new List<List<char>>();

            stacks.Add(new List<char> { 'V', 'N', 'F', 'S', 'M', 'P', 'H', 'J' });
            stacks.Add(new List<char> { 'Q', 'D', 'J', 'M', 'L', 'R', 'S' });
            stacks.Add(new List<char> { 'B', 'W', 'S', 'C', 'H', 'D', 'Q', 'N' });
            stacks.Add(new List<char> { 'L', 'C', 'S', 'R' });
            stacks.Add(new List<char> { 'B', 'F', 'P', 'T', 'V', 'M' });
            stacks.Add(new List<char> { 'C', 'N', 'Q', 'R', 'T' });
            stacks.Add(new List<char> { 'R', 'V', 'G' });
            stacks.Add(new List<char> { 'R', 'L', 'D', 'P', 'S', 'Z', 'C' });
            stacks.Add(new List<char> { 'F', 'B', 'P', 'G', 'V', 'J', 'S', 'D' });

            List<int> temp = new List<int>();
            int moveQty = 0;
            int startStackNum = 0;
            int endStackNum = 0;
            char crateToMove;

            foreach (string l in lines)
            {
                foreach (char c in l)
                {
                    if (Char.IsDigit(c))
                    {                        
                        temp.Add(int.Parse(c.ToString()));
                    }
                }

                if (temp.Count == 4)
                {
                    int tens = temp[0] * 10;
                    temp.RemoveAt(0);
                    temp[0] += tens;
                }

                moveQty = temp[0];
                startStackNum = temp[1] - 1;
                endStackNum = temp[2] - 1;

                // Part 1 moving logic

                /*for (int i = 0; i < moveQty; i++)
                {
                    if (stacks[startStackNum].Count > 0)
                    {
                        crateToMove = stacks[startStackNum][0];
                        stacks[startStackNum].RemoveAt(0);
                        stacks[endStackNum].Insert(0, crateToMove);
                    }
                }*/

                // Part 2 moving logic

                for (int i = moveQty - 1; i >= 0; i--)
                {
                    if (stacks[startStackNum].Count > 0)
                    {
                        crateToMove = stacks[startStackNum][i];
                        stacks[startStackNum].RemoveAt(i);
                        stacks[endStackNum].Insert(0, crateToMove);
                    }
                }

                temp.Clear();
            }

            string finalTopCrate = "";

            foreach (var s in stacks)
            {
                finalTopCrate += s[0].ToString();
            }

            Console.WriteLine(finalTopCrate);

        }
    }
}
