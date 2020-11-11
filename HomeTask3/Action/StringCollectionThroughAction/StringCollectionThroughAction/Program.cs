using System;

namespace StringCollectionThroughAction
{
    class Program
    {
        static void Main(string[] args)
        {
            AlphaNumbericCollector alphaNumbericCollector = new AlphaNumbericCollector();
            StringCollector stringCollector = new StringCollector();

            while (true)
            {
                Console.WriteLine("Enter string");

                string inputString = Console.ReadLine();

                bool isNumberContaining = false;

                foreach (char ch in inputString)
                {
                    if (char.IsDigit(ch))
                    {
                        isNumberContaining = true;
                        break;
                    }
                }

                if (isNumberContaining)
                {
                    Action<string> collectorAction = alphaNumbericCollector.Add;
                    alphaNumbericCollector.DoTask(collectorAction, inputString);
                }
                else
                {
                    Action<string> collectorAction = stringCollector.Add;
                    stringCollector.DoTask(collectorAction, inputString);
                }

                Console.WriteLine("AlphaNumbericCollector:");
                alphaNumbericCollector.Print();

                Console.WriteLine("StringCollector:");
                stringCollector.Print();

                while (true)
                {
                    Console.WriteLine("Continue?(Y/N)");
                    var continuation = Console.ReadKey();
                    Console.Write("\n");
                    if (continuation.KeyChar == 'N')
                        return;
                    else if (continuation.KeyChar == 'Y')
                        break;
                    Console.WriteLine("Invalid character. Enter again.");
                }
            }
        }
    }
}
