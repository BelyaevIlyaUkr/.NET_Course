using System;

namespace StringCollectingThroughEvents
{
    public delegate void AlphaNumbericCollectorPublisher(string inputString);

    public delegate void StringCollectorPublisher(string inputString);

    class Program
    {
        public static event AlphaNumbericCollectorPublisher alphaNumbericCollectorPublisher;

        public static event StringCollectorPublisher stringCollectorPublisher;

        static void Main(string[] args)
        {
            AlphaNumbericCollector alphaNumbericCollector = new AlphaNumbericCollector();
            StringCollector stringCollector = new StringCollector();

            alphaNumbericCollectorPublisher += alphaNumbericCollector.Add;
            stringCollectorPublisher += stringCollector.Add;

            while (true)
            {
                Console.WriteLine("Enter string");

                string inputString = Console.ReadLine();

                bool isNumberContaining = false;

                foreach(char ch in inputString)
                {
                    if (char.IsDigit(ch))
                    {
                        isNumberContaining = true;
                        break;
                    }
                }

                if (isNumberContaining)
                    alphaNumbericCollectorPublisher.Invoke(inputString);
                else
                    stringCollectorPublisher.Invoke(inputString);

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
