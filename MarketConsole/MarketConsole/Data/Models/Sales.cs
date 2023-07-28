using MarketConsole.Data.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketConsole.Data.Models
{
    public class Sale : BaseEntity
    {
        private static int count = 0;
        public Sale(decimal price, int quantity, DateTime dateTime)
        {
            Price = price;
            Items = new List<SaleItem>();
            DateTime = dateTime;
            Quantity = quantity;

            ID = count;
            count++;
        }
        public Sale(decimal price, int quantity, DateTime dateTime, List<SaleItem> items)
        {
            Price = price;
            Items = items;
            DateTime = dateTime;
            Quantity = quantity;

            ID = count;
            count++;
        }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; }
        public List<SaleItem> Items { get; set; }

        public void AddSaleItem(SaleItem saleItem)
        {
            Items.Add(saleItem);
        }
       

    }
   
    public class SaleItem : BaseEntity
    {
        private static int count = 0;
        public Product SalesProduct {get; set; }
        public int Quantity { get; set; }
          

       public SaleItem(Product product, int quantity) 
        {
            SalesProduct = product;
            Quantity = quantity;
          
            ID = count;
            count++;
        }
        
    }
    
}
































