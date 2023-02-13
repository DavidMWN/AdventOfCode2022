using System.Collections.Generic;

namespace AoC22Day11_MonkeyInTheMiddle
{
    class Monkey
    {
        private List<ulong> itemsHeld;
        private char operation;
        private int operationModifier;
        private int testDivisor;
        private int nextMonkeyIndexTrue;
        private int nextMonkeyIndexFalse;
        private int inspectionCount;
        private bool part1;
        private int cycleLength = -1;

        public Monkey(List<ulong>itemList, char op, int opMod, int testDiv, int trueIndex, int falseIndex, bool partOne)
        {
            itemsHeld = new List<ulong>(itemList);
            operation = op;
            operationModifier = opMod;
            testDivisor = testDiv;
            nextMonkeyIndexTrue = trueIndex;
            nextMonkeyIndexFalse = falseIndex;
            inspectionCount = 0;
            part1 = partOne;
        }

        public void InspectAndThrow(List<Monkey> monkeys)
        {
            for (int i = 0; i < itemsHeld.Count; i++)
            {
                itemsHeld[i] = Inspect(itemsHeld[i]);

                if (itemsHeld[i] % (ulong)testDivisor == 0)
                    monkeys[nextMonkeyIndexTrue].CatchItem(itemsHeld[i]);
                else
                    monkeys[nextMonkeyIndexFalse].CatchItem(itemsHeld[i]);
            }

            itemsHeld.Clear();
        }

        public void CatchItem(ulong item)
        {
            itemsHeld.Add(item);
        }

        public int GetInspectionCount()
        {
            return inspectionCount;
        }

        private ulong Inspect(ulong item)
        {      
            switch (operation)
            {
                case '+':
                    if (operationModifier == -1)
                    {
                        item += item;
                        break;
                    }
                    item += (ulong)operationModifier;
                    break;
                case '*':
                    if (operationModifier == -1)
                    {
                        item *= item;
                        break;
                    }
                    item *= (ulong)operationModifier;
                    break;
            }

            inspectionCount++;

            if (part1)
                return (ulong)(item / 3);
            else
                return item % (ulong)cycleLength;
        }

        public void SetCycleLength(int newCycleLength)
        {
            cycleLength = newCycleLength;
        }
    }
}
