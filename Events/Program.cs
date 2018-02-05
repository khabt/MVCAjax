using System;

namespace Events
{
    public delegate void PriceChangedHanlder(decimal oldPrice, decimal newPrice);

    class Product
    {
        private decimal _price;
        public event PriceChangedHanlder PriceChanged;

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price == value)
                    return;
                decimal oldPrice = _price;
                _price = value;

                if (PriceChanged != null)
                {
                    PriceChanged(oldPrice, _price);
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Product product = new Product();
            product.Price = 1500;
            product.PriceChanged += product_PriceChanged;
            product.Price = 2300;
            Console.ReadKey();
        }

        public static void product_PriceChanged(decimal oldPrice, decimal newPrice)
        {
            Console.WriteLine("Old price: {0}", oldPrice);
            Console.WriteLine("New price: {0}", newPrice);
        }
    }
}
