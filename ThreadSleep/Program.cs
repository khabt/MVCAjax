using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSleep
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(A);
            thread.Start();
            B();
        }

        static void A()
        {
            Thread.Sleep(5000);
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("A");
            }
        }

        static void B()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("B");
            }
        }
    }
}
