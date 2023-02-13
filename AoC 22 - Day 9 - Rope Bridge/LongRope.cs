using System;
using System.Collections.Generic;

namespace AoC22Day9_RopeBridge
{
    class LongRope
    {
        private int xPosition;
        private int yPosition;

        public List<RopeKnot> knots = new List<RopeKnot>();

        public LongRope()
        {
            xPosition = 0;
            yPosition = 0;

            for (int i = 0; i < 9; i++)
            {
                knots.Add(new RopeKnot());
            }
        }

        private void SetPosition(int x, int y)
        {
            xPosition = x;
            yPosition = y;

            knots[0].NextMove(x, y);

            for (int i = 1; i < 9; i++)
            {
                knots[i].NextMove(knots[i-1].xPosition, knots[i-1].yPosition);
            }
        }

        public void MoveHead(string direction, int moves)
        {
            for (int i = 1; i <= moves; i++)
            {
                switch (direction)
                {
                    case "U":
                        SetPosition(xPosition, yPosition + 1);
                        break;
                    case "D":
                        SetPosition(xPosition, yPosition - 1);
                        break;
                    case "L":
                        SetPosition(xPosition - 1, yPosition);
                        break;
                    case "R":
                        SetPosition(xPosition + 1, yPosition);
                        break;
                }
            }
        }
    }
}