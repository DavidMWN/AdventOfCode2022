using System;
using System.Collections.Generic;

namespace AoC22Day13_DistressSignal
{
    public static class ListParser
    {
        public static Queue<char> StringToQueue(string toParse)
        {
            Queue<char> queue = new Queue<char>();

            foreach (char c in toParse)
            {
                queue.Enqueue(c);
            }

            return queue;
        }

        public static List<object> Parse(string toParse)
        {
            Queue<char> queue = StringToQueue(toParse);
            List<object> list = ParseList(queue);
            return list;
        }

        public static int ParseInt(Queue<char> queue)
        {
            string temp = string.Empty;

            // If the next element in the queue is a digit, add it to the temp string
            // This is what allows multi-digit numbers to be parsed correctly
            while (char.IsDigit(queue.Peek()))
            {
                // Adds element to the temp string and removes from the queue
                temp += queue.Dequeue();
            }

            // Returns the temp string parsed as an int
            return int.Parse(temp);

        }

        public static List<object> ParseList(Queue<char> queue)
        {
            List<object> temp = new ();

            // Remove '[' from the queue
            queue.Dequeue();

            // Loops until the end of the list is found
            while (queue.Peek() != ']')
            {
                // Removes the ',' to allow the next element to be parsed into an int
                if (queue.Peek() == ',')
                {
                    queue.Dequeue();
                }

                // Parse the next element into an integer, to be added to the temp list
                object ob = ParseElement(queue);
                temp.Add(ob);                
            }

            // Remove ']' from the queue
            queue.Dequeue();

            return temp;
        }

        public static object ParseElement(Queue<char> queue)
        {
            char next = queue.Peek();

            if (char.IsDigit(next))
                return ParseInt(queue);
            else if (next == '[')
                return ParseList(queue);
            else
                throw new Exception($"Expected an int or list but found: {string.Join("", queue)}");
        }
    }
}
