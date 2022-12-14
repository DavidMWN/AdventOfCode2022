namespace AoC22Day9_RopeBridge
{
    class RopeHead
    {
        private int xPosition;
        private int yPosition;

        public RopeKnot tail = new RopeKnot();

        public RopeHead()
        {
            xPosition = 0;
            yPosition = 0;
        }

        private void SetPosition(int x, int y)
        {       
            xPosition = x;
            yPosition = y;

            tail.NextMove(x, y);
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
