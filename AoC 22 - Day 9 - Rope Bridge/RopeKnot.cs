using System;
using System.Collections.Generic;

namespace AoC22Day9_RopeBridge
{
    class RopeKnot
    {
        public int xPosition { get; set; }
        public int yPosition { get; set; }

        private List<string> positionHistory = new List<string>();

        public RopeKnot()
        {
            SetPosition(0, 0);
        }

        private void SetPosition(int x, int y)
        {
            xPosition = x;
            yPosition = y;

            AddToHistory(xPosition, yPosition);
        }

        public void NextMove(int x, int y)
        {          
            // Pure lateral movement
            if (Math.Abs(x - xPosition) > 1 && y == yPosition)
            {
                if (x > xPosition)
                    SetPosition(xPosition + 1, yPosition);

                if (x < xPosition)
                    SetPosition(xPosition - 1, yPosition);

                return;
            }

            // Pure vertical movement
            if (Math.Abs(y - yPosition) > 1 && x == xPosition)
            {
                if (y > yPosition)
                    SetPosition(xPosition, yPosition + 1);

                if (y < yPosition)
                    SetPosition(xPosition, yPosition - 1);

                return;
            }

            // Diagonal movement
            if (Math.Abs(x - xPosition) > 1 && y != yPosition)
            {
                if (x > xPosition && y > yPosition)
                    SetPosition(xPosition + 1, yPosition + 1);

                if (x < xPosition && y > yPosition)
                    SetPosition(xPosition - 1, yPosition + 1);

                if (x > xPosition && y < yPosition)
                    SetPosition(xPosition + 1, yPosition - 1);

                if (x < xPosition && y < yPosition)
                    SetPosition(xPosition - 1, yPosition - 1);

                return;
            }

            if (Math.Abs(y - yPosition) > 1 && x != xPosition)
            {
                if (x > xPosition && y > yPosition)
                    SetPosition(xPosition + 1, yPosition + 1);

                if (x < xPosition && y > yPosition)
                    SetPosition(xPosition - 1, yPosition + 1);

                if (x > xPosition && y < yPosition)
                    SetPosition(xPosition + 1, yPosition - 1);

                if (x < xPosition && y < yPosition)
                    SetPosition(xPosition - 1, yPosition - 1);

                return;
            }
        }

        // History management

        private void AddToHistory(int x, int y)
        {
            if (!positionHistory.Contains(x + " " + y))
                positionHistory.Add(x + " " + y);            
        }

        public int GetHistoryCount()
        {
            return positionHistory.Count;
        }
    }
}
