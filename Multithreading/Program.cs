using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    class Program
    {
        static void A()
        {
            for (int i = 0; i <= 100; i++)
            {
                Console.WriteLine("A : {0}", i);
            }
            Console.WriteLine("A is Done");
        }

        static void B()
        {
            for (int i = 0; i <= 100; i++)
            {
                Console.WriteLine("B : {0}", i);
            }
            Console.WriteLine("B is Done");
        }

        static void Main(string[] args)
        {
            ThreadStart ts1 = new ThreadStart(A);
            ThreadStart ts2 = new ThreadStart(B);

            Thread t1 = new Thread(ts1);
            Thread t2 = new Thread(ts2);

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine("A and B is done");
            Console.ReadKey();
        }
    }
}
