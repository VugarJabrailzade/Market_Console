using MarketConsole.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketConsole.Data.Models
{
    public class Sale : BaseEntity
    {
        private static int count = 0;
        public Sale(decimal price, List<SaleItem> items, DateTime dateTime) 
        {
            Price = price;
            Items = items;
            DateTime = dateTime;

            ID = count;
            count++;
        }
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; }
        public List<SaleItem> Items { get; set; }

    }
    public class SaleItem : BaseEntity
    {
        private static int count = 0;
        public Product SalesProduct {get; set; }
        public int Quanity { get; set; }
          

       public SaleItem(Product product, int quanity) 
        {
            SalesProduct = product;
            Quanity = quanity;

            ID = count;
            count++;
        }
    }
}
































