using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC22Day10_CathodeRayTube
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1

            List<string> lines = new List<string>();

            lines = File.ReadAllLines("../../../ProgramInstructions.txt").ToList();

            int cyclecount = 0;

            int registerX = 1;

            int sumSignalStrengths = 0;

            string[] instruction = new string[2];

            foreach (string l in lines)
            {
                if (l == "noop")
                {
                    cyclecount++;
                    if (CheckCycleTrigger(cyclecount))
                        sumSignalStrengths += ReportSignalStrength(cyclecount, registerX);
                    continue;
                }

                instruction = l.Split(" ");

                cyclecount++;
                if (CheckCycleTrigger(cyclecount))
                    sumSignalStrengths += ReportSignalStrength(cyclecount, registerX);
                               
                cyclecount++;
                if (CheckCycleTrigger(cyclecount))
                    sumSignalStrengths += ReportSignalStrength(cyclecount, registerX);

                registerX += int.Parse(instruction[1]);
            }

            Console.WriteLine(sumSignalStrengths);

            // Part 2

            cyclecount = 0;
            registerX = 1;
            int currentRowCounter = 0;

            char[] screen = new char[240];

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i] == "noop")
                {                    
                    screen[cyclecount] = DrawPixel(currentRowCounter, registerX);
                    cyclecount++;
                    currentRowCounter = UpdateRowcounter(currentRowCounter);                    
                    continue;
                }

                instruction = lines[i].Split(" ");
                                                
                screen[cyclecount] = DrawPixel(currentRowCounter, registerX);
                cyclecount++;
                currentRowCounter = UpdateRowcounter(currentRowCounter);
                                
                screen[cyclecount] = DrawPixel(currentRowCounter, registerX);
                cyclecount++;
                currentRowCounter = UpdateRowcounter(currentRowCounter);

                registerX += int.Parse(instruction[1]);
            }

            for (int i = 0; i < screen.Length; i++)
            {
                Console.Write(screen[i]);

                if ((i+1) % 40 == 0)
                    Console.Write("\n");
            }
        }

        static bool CheckCycleTrigger(int cycleCount)
        {
            switch (cycleCount)
            {
                case 20:
                    return true;
                case 60:
                    return true;
                case 100:
                    return true;
                case 140:
                    return true;
                case 180:
                    return true;
                case 220:
                    return true;
                default:
                    return false;
            }
        }

        static int ReportSignalStrength(int cycleCount, int registerX)
        {
            return cycleCount * registerX;
        }

        static int UpdateRowcounter(int rowCounter)
        {
            if (rowCounter + 1 > 39)
                return 0;

            return rowCounter + 1;
        }

        static char DrawPixel(int rowCounter, int registerX)
        {
            if (rowCounter == registerX - 1 || rowCounter == registerX || rowCounter == registerX + 1)
                return '#';

            return '.';
        }
    }
}
