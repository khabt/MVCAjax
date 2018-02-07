using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrList();
            Line();
            HashTable();
            Line();
            StackE();
            StopDisplay();
        }

        static void Line()
        {
            Console.WriteLine("_______");
        }

        static void StopDisplay()
        {
            Console.ReadKey();
        }
        static void ArrList()
        {
            ArrayList arrList = new ArrayList();
            arrList.Add(12);
            arrList.Add(1);
            arrList.Add(112);
            arrList.Add(-1);

            arrList.Sort();
            for (int i = 0; i < arrList.Count; i++)
            {
                Console.WriteLine(arrList[i]);
            }

            arrList.RemoveAt(1);
            Line();

            foreach (var item in arrList)
            {
                Console.WriteLine(item);
            }

            //Console.ReadKey();
        }

        static void HashTable()
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("A", 12);
            hashTable.Add("B", 13);
            hashTable.Add("C", 14);
            hashTable.Add("D", 15);
            hashTable.Add("E", 16);

            if (hashTable.ContainsKey("A"))
            {
                Console.WriteLine(hashTable["F"]);
            }
        }

        static void StackE()
        {
            Stack stack = new Stack();
            stack.Push("A");
            stack.Push("D");
            stack.Push("G");
            stack.Push("L");

            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
            Line();
            stack.Pop();
            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
            Line();
            stack.Clear();

            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
        }
    }
}
