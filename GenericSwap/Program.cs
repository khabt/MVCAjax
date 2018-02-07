using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericSwap
{
    class Program
    {
        static void Main(string[] args)
        {
            Changer<int> change = new Changer<int>();
            int a = 10;
            int b = 12;

            Console.WriteLine("a = {0}, b = {1}", a, b);
            change.Swap(ref a, ref b);
            Console.WriteLine("a = {0}, b = {1}", a, b);

            Changer<string> change1 = new Changer<string>();

            string str1 = "Acac";
            string str2 = "sds";

            Console.WriteLine("str1 = {0}, str2 = {1}", str1, str2);
            change1.Swap(ref str1, ref str2);
            Console.WriteLine("str1 = {0}, str2 = {1}", str1, str2);

            Console.ReadKey();
        }
    }

    class Changer<T>
    {
        public void Swap(ref T a, ref T b)
        {
            T temp;
            temp = a;
            a = b;
            b = temp;
        }
    }
}
