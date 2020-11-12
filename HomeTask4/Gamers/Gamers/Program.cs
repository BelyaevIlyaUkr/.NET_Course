using System;
using System.Linq;

namespace Gamers
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputString = "Davis, Clyne, Fonte, Hooiveld, Shaw, Davis, Schneiderlin, Cork, Lallana, Rodriguez, Lambert";

            string[] splittedString = inputString.Split(", ");
            
            var changedSplittedString = Enumerable.Range(1, splittedString.Length).Zip(splittedString, (first, second) => first + ". " + second);
            
            string resultString = string.Join(", ", changedSplittedString);
            
            Console.WriteLine(resultString);

            Console.ReadKey();
        }
    }
}
