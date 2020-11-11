using System;
using System.Collections.Generic;
using System.Text;

namespace StringCollectingThroughEvents
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
