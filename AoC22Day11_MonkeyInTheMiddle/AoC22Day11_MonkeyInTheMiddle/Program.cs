using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC22Day11_MonkeyInTheMiddle
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1

            List<string> puzzleInput = new List<string>();
            puzzleInput = File.ReadAllLines("../../../MonkeyInput.txt").ToList();

            char[] delimiterChars = { ' ', ':', ',' };

            // Value placeholders for Monkey constructor
            List<ulong> itemsList = new List<ulong>();
            char operation;
            int operationModifier;
            int testDivisor;
            int trueIndex;
            int falseIndex;

            List<Monkey> monkeyList = new List<Monkey>();

            // Creating a second list and a list of the divisors here for a clean start on Part 2
            List<Monkey> monkeyList2 = new List<Monkey>();
            List<int> divisors = new List<int>();

            // Parse puzzle input and create Monkeys
            for (int i = 0; i < puzzleInput.Count; i++)
            {
                if (puzzleInput[i].StartsWith("Monkey"))
                {
                    string[] startingItemsLine = puzzleInput[i + 1].Split(delimiterChars);
                    for (int x = 5; x < startingItemsLine.Length; x++)
                    {
                        ulong number;
                        bool success = ulong.TryParse(startingItemsLine[x], out number);
                        if (success)
                            itemsList.Add(number);
                    }

                    string[] operationLine = puzzleInput[i + 2].Split(delimiterChars);
                    operation = Char.Parse(operationLine[operationLine.Length - 2]);
                    {
                        int number2;
                        bool success = int.TryParse(operationLine[operationLine.Length - 1], out number2);
                        if (success)
                            operationModifier = int.Parse(operationLine[operationLine.Length - 1]);
                        else
                            operationModifier = -1;
                    }

                    string[] testLine = puzzleInput[i + 3].Split(delimiterChars);
                    testDivisor = int.Parse(testLine[testLine.Length - 1]);
                    divisors.Add(testDivisor); // For part 2

                    string[] trueLine = puzzleInput[i + 4].Split(delimiterChars);
                    trueIndex = int.Parse(trueLine[trueLine.Length - 1]);

                    string[] falseLine = puzzleInput[i + 5].Split(delimiterChars);
                    falseIndex = int.Parse(falseLine[falseLine.Length - 1]);

                    monkeyList.Add(new Monkey(itemsList, operation, operationModifier, testDivisor, trueIndex, falseIndex, true));
                    monkeyList2.Add(new Monkey(itemsList, operation, operationModifier, testDivisor, trueIndex, falseIndex, false)); // For part 2

                    itemsList.Clear();
                }
            }

            // 20 rounds of inspecting and throwing
            for (int i = 0; i < 20; i++)
            {
                foreach (Monkey m in monkeyList)
                    m.InspectAndThrow(monkeyList);
            }

            // Getting inspection counts and determining monkey business
            List<int> inspectionCount = new List<int>();

            foreach (Monkey m in monkeyList)
                inspectionCount.Add(m.GetInspectionCount());

            inspectionCount.Sort();

            int monkeyBusiness = inspectionCount[inspectionCount.Count - 1] * inspectionCount[inspectionCount.Count - 2];

            Console.WriteLine(monkeyBusiness);

            // Part 2

            // *** See the comment section underneath ***

            int cycleLength = 1;

            for (int i = 0; i < divisors.Count; i++)
            {
                cycleLength *= divisors[i];
            }

            foreach (Monkey m in monkeyList2)
            {
                m.SetCycleLength(cycleLength);
            }

            // 10000 rounds of inspecting and throwing
            for (int i = 0; i < 10000; i++)
            {
                foreach (Monkey m in monkeyList2)
                    m.InspectAndThrow(monkeyList2);
            }

            // Get new inspection counts and determine monkey business
            List<int> inspectionCount2 = new List<int>();

            foreach (Monkey m in monkeyList2)
                inspectionCount2.Add(m.GetInspectionCount());

            inspectionCount2.Sort();

            ulong monkeyBusiness2 = (ulong)inspectionCount2[inspectionCount2.Count - 1] * (ulong)inspectionCount2[inspectionCount2.Count - 2];

            Console.WriteLine(monkeyBusiness2);
        }
    }
}

/*
 *** I did not understand the wording of part 2 or that the solution was mathematical in nature, and needed to look for more plain hints.
 ***
 *** This explanation of how to approach the solution to Part 2 comes from MattieShoes on reddit: https://www.reddit.com/r/adventofcode/comments/zizi43/comment/iztla33/
  
 
There's different levels of understanding, but... We can start from easy.

If we're testing for divisibility by 2

  0 [2]  1 []
  2 [2]  3 []
  4 [2]  5 []
  6 [2]  7 []
...
It's pretty easy to spot the pattern -- it repeats after 2 entries. So you can alter the dividend (worry) by adding or subtracting 2 as much as you like, and it won't affect the result of the divisibility test.

Then you have another monkey that tests for divisibility by three though, so adding or subtracting by 2 would affect the later disposition of that item if it ever goes to that monkey. So we need to find the number we could add or subtract by that won't affect divisibility by either

  0 [2, 3]  1 []  2 [2]  3 [3]  4 [2]  5 []
  6 [2, 3]  7 []  8 [2]  9 [3] 10 [2] 11 []
 12 [2, 3] 13 [] 14 [2] 15 [3] 16 [2] 17 []
 18 [2, 3] 19 [] 20 [2] 21 [3] 22 [2] 23 []
...
You can see there's a pattern and it repeats after 6 entries. So you can alter the dividend (worry) by adding or subtracting 6 freely and it won't affect the divisibility test for either monkey. Now 6 happens to be 2 x 3 so you might want to check that, say, using 3 and 5.

  0 [3, 5]  1 []  2 []  3 [3]  4 []  5 [5]  6 [3]  7 []  8 []  9 [3] 10 [5] 11 [] 12 [3] 13 [] 14 []
 15 [3, 5] 16 [] 17 [] 18 [3] 19 [] 20 [5] 21 [3] 22 [] 23 [] 24 [3] 25 [5] 26 [] 27 [3] 28 [] 29 []
 30 [3, 5] 31 [] 32 [] 33 [3] 34 [] 35 [5] 36 [3] 37 [] 38 [] 39 [3] 40 [5] 41 [] 42 [3] 43 [] 44 []
...
Sho nuff, the cycle is 15.

Now for certain pairs of numbers, the pattern will be shorter than the product of the numbers -- e.g. for 2 and 4, the pattern is 4 long, not 8 -- but the shorter pattern always fits evenly into the product, so using the product still works. So there's a lot of stuff you can uncover (https://en.wikipedia.org/wiki/Coprime_integers) but you don't need it to get the right answer.

So at this point, you can solve the problem without using modulo at all -- get the cycle length for all 8 monkeys combined (the product of their divisors), then you could do something ghetto like

while worry > cycle_length:
    worry -= cycle_length
and you know you haven't affected any of the divisibility tests.

Modulo worry = worry % cycle_length is just turning the loop above into a single operation. It's prettier, but it's the same thing.
*/