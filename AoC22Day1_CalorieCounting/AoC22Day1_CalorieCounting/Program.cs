using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AoC22Day1_CalorieCounting
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1

            string filePath = "../../../CaloriesInput.txt";

            List<string> lines = new List<string>();
            lines = File.ReadAllLines(filePath).ToList();

            List<int> calories = new List<int>();
            List<int> elfCalories = new List<int>();
            int tempCalories = 0;
            
            foreach (string l in lines)
            {
                if (!String.IsNullOrWhiteSpace(l))
                    calories.Add(Convert.ToInt32(l));
                else
                {
                    foreach (int c in calories)
                        tempCalories += c;

                    elfCalories.Add(tempCalories);

                    tempCalories = 0;

                    calories.Clear();
                }
            }

            Console.WriteLine(elfCalories.Max());

            // Part 2

            elfCalories.Sort();
            elfCalories.Reverse();

            int topThreeElfCalories = 0;

            for (int i = 0; i < 3; i++)
            {
                topThreeElfCalories += elfCalories[i];
            }

            Console.WriteLine(topThreeElfCalories);
        }
    }
}
