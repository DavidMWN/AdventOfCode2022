using System;
using System.Collections.Generic;
using System.Text;

namespace AoC22Day3_RucksacReorg
{
    class Priorities
    {
        private char[] priorityReference = new char[52];

        public Priorities()
        {
            priorityReference = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        }

        public int GetPriority(char item)
        {            
            for (int i = 0; i < priorityReference.Length; i++)
            {
                if (item == priorityReference[i])
                {                    
                    return i+1;
                }
            }

            return -1;
        }
    }
}
