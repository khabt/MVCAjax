using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDeadLock
{
    class Program
    {
        static object syncObj1 = new object();
        static object syncObj2 = new object();
        static void Main(string[] args)
        {
            Thread t1 = new Thread(A);
            Thread t2 = new Thread(B);

            t1.Start();
            t2.Start();
            Console.ReadKey();
        }

        static void A()
        {
            Console.WriteLine("Inside A");
            lock (syncObj1)
            {
                Console.WriteLine("A: Lock syncObj1");
                Thread.Sleep(100);
                lock (syncObj2)
                {
                    Console.WriteLine("A: Lock syncObj2");
                }
            }
        }

        static void B()
        {
            Console.WriteLine("Inside A");
            lock (syncObj2)
            {
                Console.WriteLine("B: Lock syncObj2");
                Thread.Sleep(100);
                lock (syncObj1)
                {
                    Console.WriteLine("A: Lock syncObj1");
                }
            }
        }
    }
}
