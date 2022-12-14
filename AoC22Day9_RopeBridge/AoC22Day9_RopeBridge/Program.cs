using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AoC22Day9_RopeBridge
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1

            List<string> lines = new List<string>();

            lines = File.ReadAllLines("../../../MotionInput.txt").ToList();

            RopeHead ropeHead = new RopeHead();
            string[] motion = new string[2];

            foreach (string l in lines)
            {
                motion = l.Split(" ");

                ropeHead.MoveHead(motion[0], int.Parse(motion[1].ToString()));
            }

            Console.WriteLine(ropeHead.tail.GetHistoryCount());

            // Part 2

            LongRope longRope = new LongRope();

            foreach (string l in lines)
            {
                motion = l.Split(" ");

                longRope.MoveHead(motion[0], int.Parse(motion[1].ToString()));
            }

            Console.WriteLine(longRope.knots[8].GetHistoryCount());
        }
    }
}
