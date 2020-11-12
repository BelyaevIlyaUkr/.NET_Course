using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace Gamers_Sorted_By_Date
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputString = "Jason Puncheon, 26/06/1986; Jos Hooiveld, 22/04/1983; Kelvin Davis, 29/09/1976; Luke Shaw, 12/07/1995; Gaston Ramirez, 02/12/1990; Adam Lallana, 10/05/1988";

            string[] splittedString = inputString.Split("; ");

            var gamersCollection = splittedString
                .Select(gamer => gamer.Split(", "))
                .Select(gamer => new { Name = gamer[0], Date = DateTime.Parse(gamer[1], CultureInfo.CreateSpecificCulture("fr-FR")) });

            var sortedGamers = gamersCollection.OrderBy(gamer => DateTime.Today.Subtract(gamer.Date));

            foreach(var gamer in sortedGamers)
            {
                Console.WriteLine("Name = {0}, Date of Birth = {1}, Age = {2}", gamer.Name, gamer.Date.ToString("d", new CultureInfo("fr-FR")), DateTime.Today.Subtract(gamer.Date).Days/365);
            }

            Console.ReadKey();
        }
    }
}
