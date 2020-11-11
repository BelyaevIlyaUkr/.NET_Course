using System;
using System.Collections.Generic;

namespace StringCollectingThroughDelegates
{
    class StringCollector
    {
        private List<string> strings = new List<string>();

        public void Add(string inputString)
        {
            strings.Add(inputString);
        }

        public void Print()
        {
            foreach (var str in strings)
            {
                Console.WriteLine($"{str}");
            }
        }
    }
}
