using System;
using System.Collections.Generic;
using System.Text;

namespace StringCollectionThroughAction
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

        public void DoTask(Action<string> collectorAction, string inputString)
        {
            collectorAction.Invoke(inputString);
        }
    }
}
