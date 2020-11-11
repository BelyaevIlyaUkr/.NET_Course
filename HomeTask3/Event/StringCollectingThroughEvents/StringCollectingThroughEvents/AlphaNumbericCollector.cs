using System;
using System.Collections.Generic;
using System.Text;

namespace StringCollectingThroughEvents
{   
    class AlphaNumbericCollector
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
