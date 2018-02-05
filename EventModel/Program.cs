using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventModel
{
    public class PriceChangedEventAgrs
    {
        public readonly decimal OldPrice;
        public readonly decimal NewPrice;

        public PriceChangedEventAgrs(decimal oldPrice, decimal newPrice)
        {
            this.OldPrice = oldPrice;
            this.NewPrice = newPrice;
        }
    }

    public delegate void PriceChangedHanlder(object sender, PriceChangedEventAgrs e);

    class Product
    {
        public decimal _price;
        public event PriceChangedHanlder PriceChanged;

        public void OnPriceChange(PriceChangedEventAgrs e)
        {
            PriceChanged?.Invoke(this, e);
            //if (PriceChanged != null)
            //{
            //    PriceChanged(this, e);
            //}
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price == value)
                    return;
                decimal oldPrice = _price;
                _price = value;
                OnPriceChange(new PriceChangedEventAgrs(oldPrice, _price));
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Product product = new Product();
            product.Price = 4000;
            product.PriceChanged += product_PricecChanged;
            product.Price = 5000;
            Console.ReadKey();
        }

        public static void product_PricecChanged(object sender, PriceChangedEventAgrs e)
        {
            Console.WriteLine("Old price: " + e.OldPrice);
            Console.WriteLine("New price: " + e.NewPrice);
        }
    }
}
