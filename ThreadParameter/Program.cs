using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadParameter
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread ts1 = new Thread(DoWork);
            ts1.Start(1);

            Program program = new Program();
            Thread ts = new Thread(program.DoMoreWork);
            ts.Start("TEDU");

            Console.ReadKey();
        }

        static void DoWork(object data)
        {
            Console.WriteLine("This is static thread");
            Console.WriteLine("Parameter data: {0}", data);
        }

        void DoMoreWork(object data)
        {
            Console.WriteLine("This instance thread");
            Console.WriteLine("Parameter data: {0}", data);
        }
    }
}
