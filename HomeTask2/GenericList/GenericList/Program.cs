using System;

namespace GenericList
{
    class Program
    {
        static void Main(string[] args)
        {
            GenList<int> nmbrs = new GenList<int>();
            nmbrs.Add(6);
            nmbrs.Add(8);
            nmbrs.Add(10);
            nmbrs.Add(9);
            nmbrs.Add(1);

            Console.WriteLine("Initial List");
            foreach(var nmbr in nmbrs)
            {
                Console.Write($"{(int)nmbr} ");
            }

            Console.WriteLine($"\nIs Element 9 removed: {nmbrs.Remove(9)}");
            Console.WriteLine($"Is Element 7 removed: {nmbrs.Remove(7)}");
            
            Console.WriteLine("List after removing 9 and 7:");
            foreach (var nmbr in nmbrs)
            {
                Console.Write($"{(int)nmbr} ");
            }

            nmbrs.RemoveAt(0);

            Console.WriteLine("\nList after removing first element");
            foreach (var nmbr in nmbrs)
            {
                Console.Write($"{(int)nmbr} ");
            }

            nmbrs.RemoveAt(-1);

            Console.WriteLine($"First occurence of element less than 5: {nmbrs.Find(x => x < 5)}");

            Console.WriteLine($"Default value for returning in Find for this type:{nmbrs.Find(x => x < -1)} ");

            Console.WriteLine($"Index of first occurence of element less than 5: {nmbrs.FindIndex(x => x < 5)}");

            Console.WriteLine($"Index of first occurence of element less than -1: {nmbrs.FindIndex(x => x < -1)}");

            nmbrs.Insert(3, 12);
            nmbrs.Insert(1, 50);
            Console.WriteLine("List after inserting element 12 and 50");
            foreach (var nmbr in nmbrs)
            {
                Console.Write($"{(int)nmbr} ");
            }

            nmbrs.Insert(-3, 10);

            Console.WriteLine($"Number of removed elements {nmbrs.RemoveAll()}");

            Console.WriteLine("\nList after removing all elements");
            foreach (var nmbr in nmbrs)
            {
                Console.Write($"{(int)nmbr} ");
            }

            Console.ReadLine();
        }
    }
}
