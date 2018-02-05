using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    public delegate void NumberChange(int number);
    class Program
    {
        static int number = 10;
        static void Add(int changeNumber)
        {
            number += changeNumber;
        }

        static void Multi(int changeNumber)
        {
            number *= changeNumber;
        }
        static void Main(string[] args)
        {
            NumberChange nc1 = new NumberChange(Add);
            nc1(13);
            Console.WriteLine("Value of number: {0}", number);

            NumberChange nc2 = new NumberChange(Multi);
            nc2(10);
            Console.WriteLine("Value of number: {0}", number);
            Console.ReadKey();
        }
    }
}
