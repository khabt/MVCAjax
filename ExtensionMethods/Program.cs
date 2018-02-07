using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            while (i == 0)
            {
                Console.WriteLine("Enter your number: ");
                string input = Console.ReadLine();
                bool isNumber = input.IsNumber();
                bool isMinSize = input.IsMinSize(5);
                Console.WriteLine(isNumber);
                Console.WriteLine(isMinSize);
                if (isNumber && isMinSize) i = 1;
            }

            Console.ReadKey();
        }
    }
}
