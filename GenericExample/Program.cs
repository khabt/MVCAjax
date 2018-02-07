using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericExample
{
    class MyGeneric<T>
    {
        //public void Print(int a)
        //{
        //    Console.WriteLine(a);
        //    Console.WriteLine(a.GetType().ToString());
        //}

        //public void Print(string a)
        //{
        //    Console.WriteLine(a);
        //    Console.WriteLine(a.GetType().ToString());
        //}

        public void Print(T a)
        {
            Console.WriteLine(a);
            Console.WriteLine(a.GetType().ToString());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //MyGeneric my = new MyGeneric();
            //my.Print(1);

            //my.Print("aa");

            //Console.ReadKey();

            MyGeneric<int> my = new MyGeneric<int>();
            my.Print(1);

            MyGeneric<string> my2 = new MyGeneric<string>();
            my2.Print("aada");

            Console.ReadKey();
        }
    }
}
