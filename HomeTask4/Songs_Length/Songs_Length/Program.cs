using System;
using System.Linq;

namespace Songs_Length
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputString = "4:12,2:43,3:51,4:29,3:24,3:14,4:46,3:25,4:52,3:27";

            var songsCollection = inputString.Split(',')
                .Select(song => song.Split(':')).Select(splittedSong => new { minutes = splittedSong[0], seconds = splittedSong[1]});

            int commonSongsLengthInSeconds = songsCollection.Aggregate(0, (commonLength,song) 
                => commonLength + Convert.ToInt32(song.minutes) * 60 + Convert.ToInt32(song.seconds));

            Console.WriteLine("Common songs length is {0} minutes {1} seconds", 
                commonSongsLengthInSeconds / 60, commonSongsLengthInSeconds % 60);

            Console.ReadKey();
        }
    }
}
