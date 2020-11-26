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
                "https://dotnet.microsoft.com/learn/dotnet/what-is-dotnet",
                "https://dotnet.microsoft.com/learn/aspnet/microservices-architecture",
                "https://docs.microsoft.com/en-us/azure/aks/intro-kubernetes",
                "https://docs.microsoft.com/en-us/azure/architecture/patterns/",
                "https://docs.microsoft.com/en-us/azure/architecture/patterns/anti-corruption-layer",
                "https://docs.microsoft.com/en-us/dotnet/machine-learning/how-does-mldotnet-work",
                "https://docs.microsoft.com/en-us/windows/uwp/gaming/",
                "https://docs.microsoft.com/en-us/cognitive-toolkit/",
                "https://docs.microsoft.com/en-us/nuget/what-is-nuget",
                "https://docs.microsoft.com/en-us/ef/core/"
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

            var contentString = client.DownloadString((string)address);

            _pool.WaitOne();

            sitesContent.Add(contentString);

            Console.WriteLine(sitesContent[sitesContent.Count - 1]);

            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n");

            _pool.Release();
        }
    }
}
