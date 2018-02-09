using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                //Extension method
                Console.WriteLine("Enter your number: ");
                string input = Console.ReadLine();
                bool isNumber = input.IsNumber();
                bool isMinSize = input.IsMinSize(5);
                Console.WriteLine(isNumber);
                Console.WriteLine(isMinSize);
                if (isNumber && isMinSize) i = 1;

                List<int> intList = new List<int> { 1, 2, 3, 4, 5, 6 };
                List<double> doubleList = new List<double> { 0.4, 0.6, 0.8, 1.2 };

                var randomInt = getRandomElement(intList);
                var randonDouble = getRandomElement(doubleList);

                Console.WriteLine(randomInt);
                Console.WriteLine(randonDouble);

                //Extension method
                Console.WriteLine(intList.randomElement());
                Console.WriteLine(doubleList.randomElement());
                Line();
            }

            Console.ReadKey();
        }

        public static T getRandomElement<T>(List<T> list)
        {
            Random ran = new Random();
            int randomIndex = ran.Next(list.Count - 1);
            return list[randomIndex];
        }

        public static void Line()
        {
            Console.WriteLine("_______");
        }
    }
}
