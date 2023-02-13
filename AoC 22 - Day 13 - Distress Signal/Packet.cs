using System;
using System.Collections.Generic;

namespace AoC22Day13_DistressSignal
{
    public static class Packet
    {
        public static string ListToString(List<object> objects)
        {
            List<string> asStrings = new();

            foreach (object o in objects)
            {
                // Check if each element in the list is a list or a string
                string s = o switch
                {
                    // If a list, recurses to produce a string
                    (List<object> list) => ListToString(list),
                    // If not a list, render the element as a string
                    _ => $"{o}",
                };
                // Add string to list of strings
                asStrings.Add(s);
            }

            // Return a single string made up of the joined list of strings asStrings
            return "[" + string.Join(",", asStrings) + "]";
        }

        public static int CompareElements(object first, object second)
        {
            // Compare the elements to each other
            return (first, second) switch
            {
                // Check if they are both ints
                // If so, returns -1 if first int is smaller than second, 0 if the are equal, 1 if first is larger
                (int f, int s) => Math.Sign(f - s),

                // Check if they are both lists
                // If so, runs CompareLists function
                (List<object> f, List<object> s) => CompareLists(f, s),

                // Check for mis-match of list and int, run CompareLists with int converted to a list
                (int f, List<object> s) => CompareLists(new List<object>() { f }, s),
                (List<object> f, int s) => CompareLists(f, new List<object>() { s }),

                // Throws exception otherwise
                _ => throw new Exception($"Could not compare unknown elements {first} vs. {second}."),
            };
        }

        public static int CompareLists(List<object> first, List<object> second)
        {
            // Get maximum comparison index by comparing lengths of the lists and taking the shorter
            int maxIndex = Math.Min(first.Count, second.Count);
            
            for (int i = 0; i < maxIndex; i++)
            {
                object element0 = first[i];
                object element1 = second[i];

                int diff = CompareElements(element0, element1);

                if (diff < 0)
                    return -1;
                else if (diff > 0)
                    return 1;                
            }

            return Math.Sign(first.Count - second.Count);
        }
    }
}
