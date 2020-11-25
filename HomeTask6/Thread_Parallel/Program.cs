using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Thread_Parallel
{
    class Program
    {
        private static Semaphore _pool;

        private static List<string> sitesContent = new List<string>();

        static void Main(string[] args)
        {
            List<string> sites = new List<string> {
                "https://en.wikipedia.org/wiki/Clodia_Pulchra_(wife_of_Octavian)",
                "https://en.wikipedia.org/wiki/Battle_of_Midway",
                "https://en.wikipedia.org/wiki/Cold_War",
                "https://en.wikipedia.org/wiki/.NET_Core",
                "https://en.wikipedia.org/wiki/MIT_License",
                "https://en.wikipedia.org/wiki/C_Sharp_(programming_language)",
                "https://en.wikipedia.org/wiki/Microsoft_Visual_Studio#2019",
                "https://en.wikipedia.org/wiki/GlobalLogic",
                "https://en.wikipedia.org/wiki/Manhattan_Project",
                "https://en.wikipedia.org/wiki/The_Last_of_Us_Part_II"
            };

            _pool = new Semaphore(0, 1);

            for (int i = 0; i < sites.Count; i++)
            {
                new Thread(new ParameterizedThreadStart(DownloadString)).Start(sites[i]);
            }

            _pool.Release();

            Console.ReadKey();
        }

        private static void DownloadString(object address)
        {
            WebClient client = new WebClient();

            _pool.WaitOne();

            sitesContent.Add(client.DownloadString((string)address));

            Console.WriteLine(sitesContent[sitesContent.Count - 1]);

            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n");

            _pool.Release();
        }
    }
}
