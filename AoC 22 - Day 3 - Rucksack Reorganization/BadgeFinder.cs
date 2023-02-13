using System;
using System.Collections.Generic;
using System.Text;

namespace AoC22Day3_RucksacReorg
{
    class BadgeFinder
    {
        private string elfGroupMember1;
        private string elfGroupMember2;
        private string elfGroupMember3;

        public void SetElfGroup(string elf1, string elf2, string elf3)
        {
            elfGroupMember1 = elf1;
            elfGroupMember2 = elf2;
            elfGroupMember3 = elf3;
        }

        public char FindBadge()
        {
            for (int x = 0; x < elfGroupMember1.Length; x++)
            {
                for (int y = 0; y < elfGroupMember2.Length; y++)
                {
                    if (elfGroupMember1[x] == elfGroupMember2[y])
                    {
                        for (int z = 0; z < elfGroupMember3.Length; z++)
                        {
                            if (elfGroupMember1[x] == elfGroupMember3[z])
                            {
                                return elfGroupMember1[x];
                            }
                        }
                    }
                }
            }

            return '1';
        }
    }
}
