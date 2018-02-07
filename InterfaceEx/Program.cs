using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceEx
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    interface IManage<T>
    {
        void Add(T value);
        void Edit(T value);
        void Delete(T id);
        List<T> GetAll();
    }
    class Product
    {
        int ID { set; get; }
        string Name { set; get; }
    }
    class Manage<Product> : IManage<Product>
    {
        public void Add(Product value)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Product value)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
