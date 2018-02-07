using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LockingThread
{
    class Program
    {
        static int amount = 0;
        static object lockObj = new object();
        static void Main(string[] args)
        {
            Thread t1 = new Thread(() =>
            {
                amount++;
                for (int i = 0; i < 100; i++)
                {
                    lock (lockObj)
                    {
                        if (amount > 0)
                        {
                            Thread.Sleep(1);
                            Console.WriteLine(amount + "\t");
                        }
                    }
                }
            });

            Thread t2 = new Thread(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    lock (lockObj)
                    {
                        amount--;
                    }
                }
            });

            t1.Start();
            t2.Start();
            Console.ReadKey();
        }
    }
}
