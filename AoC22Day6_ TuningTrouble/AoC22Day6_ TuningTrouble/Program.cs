using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AoC22Day6__TuningTrouble
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1

            string filePath = "../../../TuningInput.txt";

            string input = File.ReadAllText(filePath).ToString();

            List<char> datastream = new List<char>();

            int packetMarker = 0;

            for (int i = 0; i < 4; i++)
            {
                datastream.Add(input[i]);
            }

            for (int i = 4; i < input.Length; i++)
            {
                if (FindPacketMarker(datastream))
                {
                    packetMarker = i;
                    break;
                }

                datastream.RemoveAt(0);
                datastream.Add(input[i]);
            }

            Console.WriteLine(packetMarker);

            // Part 2

            datastream.Clear();

            int messageMarker = 0;

            for (int i = 0; i < 14; i++)
            {
                datastream.Add(input[i]);
            }

            for (int i = 14; i < input.Length; i++)
            {
                if (FindMessageMarker(datastream))
                {
                    messageMarker = i;
                    break;
                }

                datastream.RemoveAt(0);
                datastream.Add(input[i]);
            }

            Console.WriteLine(messageMarker);
        }

        static bool FindPacketMarker(List<char> data)
        {            
            for (int x = 0; x < 3; x++)
            {
                for (int y = x + 1; y < 4; y++)
                {
                    if (data[x] == data[y])
                        return false;
                }
            }

            return true;
        }

        static bool FindMessageMarker(List<char> data)
        {
            for (int x = 0; x < 13; x++)
            {
                for (int y = x + 1; y < 14; y++)
                {
                    if (data[x] == data[y])
                        return false;
                }
            }

            return true;
        }
    }
}
