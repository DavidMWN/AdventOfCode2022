using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AoC22Day13_DistressSignal
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1

            List<string> puzzleInput = new List<string>();

            puzzleInput = File.ReadAllLines("../../../DistressSignalInput.txt").ToList();

            string[] packetPair = new string[2];
            int index = 1;
            int sum = 0;

            for (int i = 0; i < puzzleInput.Count; i += 3)
            {
                packetPair[0] = puzzleInput[i];
                packetPair[1] = puzzleInput[i + 1];

                List<object> packet1 = ListParser.Parse(packetPair[0].Trim());
                List<object> packet2 = ListParser.Parse(packetPair[1].Trim());

                if (Packet.CompareLists(packet1, packet2) < 0)
                    sum += index;

                index++;
            }

            Console.WriteLine(sum);

            // Part 2

            List<object> divider1 = ListParser.Parse("[[2]]");
            List<object> divider2 = ListParser.Parse("[[6]]");

            int indexDivider1 = 1;
            int indexDivider2 = 2;

            for (int i = 0; i < puzzleInput.Count; i++)
            {
                if (puzzleInput[i] == "")
                    continue;

                List<object> packetSingle = ListParser.Parse(puzzleInput[i].Trim());

                if (Packet.CompareLists(packetSingle, divider1) <= 0)
                    indexDivider1++;

                if (Packet.CompareLists(packetSingle, divider2) <= 0)
                    indexDivider2++;
            }

            Console.WriteLine(indexDivider1 * indexDivider2);
        }
    }
}
