using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp73
{
    public static class ConsoleInputHandler
    {
        public static Worker ReadWorkerInfo()
        {
            Console.Write("Enter the full name(surname, name, middlename): ");
            var fullName = Console.ReadLine().Trim().Split();

            Console.Write("Enter the occupation: ");
            var occupation = Console.ReadLine();

            Console.Write("Enter the applying year: ");
            int.TryParse(Console.ReadLine(), out int applyingYear);

            return new Worker(fullName, occupation, applyingYear);
        }
    }
}
