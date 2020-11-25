using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;


namespace Thread_Consistently
{
    class Program
    {
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

            List<Thread> threads = new List<Thread>();

            foreach (var site in sites)
            {
                threads.Add(new Thread(() => DownloadString(site)));
            }

            foreach(var t in threads)
            {
                t.Start();

                t.Join();
            }

            Console.ReadKey();
        }

        private static void DownloadString(object url)
        {
            WebClient client = new WebClient();

            sitesContent.Add(client.DownloadString((string)url));

            Console.WriteLine(sitesContent[sitesContent.Count - 1]);

            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n");
        }
    }
}
